﻿using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using TodoList.Core.Contracts;
using TodoList.Core.Models;
using TodoList.Helpers;
using TodoList.Services;

namespace TodoList.ViewModels;

public class MainViewModel : BindableBase, INavigationAware
{
    #region Fields

    private readonly ITaskItemService _taskItemService;
    private TaskItem _selected;
    private string _searchText;
    private string _filterByDate;
    private DateTime? _searchDate;
    private bool _showTodayList = true;
    #endregion

    #region Properties

    public TaskItem Selected
    {
        get { return _selected; }
        set { SetProperty(ref _selected, value); RemoveCommand?.RaiseCanExecuteChanged(); SetAsDoneCommand?.RaiseCanExecuteChanged(); Save(); }
    }

    public string SearchText
    {
        get { return _searchText; }
        set 
        {
            SetProperty(ref _searchText, value);
            Search();
        }
    }

    public DateTime? SearchDate
    {
        get { return _searchDate; }
        set
        {
            SetProperty(ref _searchDate, value);
            Search();
        }
    }

    public bool ShowTodayList
    {
        get { return _showTodayList; }
        set
        {
            SetProperty(ref _showTodayList, value);
        }
    }

    public ObservableCollection<TaskItem> Tasks { get; private set; } = new ObservableCollection<TaskItem>();

    public ICollectionView TasksCollection { get; private set; }

    public ObservableCollection<TaskItem> TodayTasks { get; private set; } = new ObservableCollection<TaskItem>();

    public string FilterByDate
    {
        get { return _filterByDate; }
        set
        {
            SetProperty(ref _filterByDate, value);
            Search();
        }
    }

    #endregion

    #region Commands

    public DelegateCommandEx AddCommand { get; }
    public DelegateCommandEx RemoveCommand { get; }
    public DelegateCommandEx SetAsDoneCommand { get;}
    public DelegateCommandEx SaveCommand { get; }
    public DelegateCommandEx SearchCommand { get; }

    public DelegateCommandEx AddSubItemCommand { get; }

    public DelegateCommandEx RemoveSubItemCommand { get; }

    public DelegateCommandEx ClearSearchCommand { get; }



    #endregion

    #region Constructors

    public MainViewModel(ITaskItemService taskItemService)
    {
        _taskItemService = taskItemService;
        AddCommand = new DelegateCommandEx(() => Add(null));
        RemoveCommand = new DelegateCommandEx(Remove, () => Selected != null);
        SetAsDoneCommand = new DelegateCommandEx((o) => SetAsDone(o as TaskItem), () => Selected != null);
        AddSubItemCommand = new DelegateCommandEx(AddSubItem, () => Selected != null);
        RemoveSubItemCommand = new DelegateCommandEx((o) => RemoveSubItem(o), () => Selected != null);
        ClearSearchCommand = new DelegateCommandEx(ClearSearch);
        SaveCommand = new DelegateCommandEx(Save);
        TasksCollection = CollectionViewSource.GetDefaultView(Tasks);
        TasksCollection.GroupDescriptions.Add(new TaskGroupComplited());
        TasksCollection.SortDescriptions.Add(new SortDescription(nameof(TaskItem.IsCompleted), ListSortDirection.Ascending));
        TasksCollection.SortDescriptions.Add(new SortDescription(nameof(TaskItem.Important), ListSortDirection.Descending));
        SearchCommand = new DelegateCommandEx(Search);
    }

    

    #endregion

    #region Navigation

    public async void OnNavigatedTo(NavigationContext navigationContext)
    {
        Tasks.Clear();
        TodayTasks.Clear();
        var data = await _taskItemService.GetAllAsync();
        foreach (var item in data)
        {
            Add(item);
        }
        _selected = Tasks.FirstOrDefault();
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
        => true;

    #endregion

    #region Commands methods

    private void ClearSearch()
    {
        _searchText = null;
        _searchDate = null;
        Search();
        RaisePropertyChanged(nameof(SearchText));
        RaisePropertyChanged(nameof(SearchDate));
    }

    private void Search()
    {
        if (String.IsNullOrEmpty(SearchText) && SearchDate == null)
        {
            TasksCollection.Filter = null;
            ShowTodayList = true;
        }
        else
        {
            TasksCollection.Filter = FilterTasks;
            ShowTodayList = false;
        }
            
    }

    private bool FilterTasks(object obj)
    {
        if (obj is TaskItem task)
        {
            if(SearchDate != null)
            {
                if (SearchDate.Value.Date == task.DueDate.Date)
                    return true;
            }

            if (!(string.IsNullOrEmpty(SearchText) || string.IsNullOrWhiteSpace(SearchText)))
            {
                string search = SearchText.ToLower();
                if (task.Title != null && task.Title.ToLower().Contains(search))
                    return true;

                if (task.Description != null && task.Description.ToLower().Contains(search))
                    return true;
            }
            return false;
        }

        return true;
    }

    private void AddSubItem()
    {
        if (Selected == null)
            return;

        var child = new TaskISubtem();
        _taskItemService.AddSubItem(Selected, child);
        Selected.RaisePropertyChanged(nameof(Selected.Items));
    }

    private void RemoveSubItem(object obj)
    {
        if (Selected == null || !(obj is TaskISubtem subitem) || !(Selected?.Items.Contains(subitem) ?? false))
            return;

        _taskItemService.RemoveSubItem(Selected, subitem);
        Selected.RaisePropertyChanged(nameof(Selected.Items));
    }

    private void Add(TaskItem item = null)
    {
        if(item == null)
        {
            item = _taskItemService.Add(new TaskItem()
            {
                Id = Guid.NewGuid(),
                DueDate = DateTime.Now.AddDays(1),
                IsCompleted = false,
            });
        }
        Tasks.Add(item);
        if (item.isToday() && !item.IsCompleted)
            TodayTasks.Add(item);
        Selected = item;
        item.PropertyChanged += Item_PropertyChanged;
    }

    private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if(e.PropertyName == nameof(TaskItem.Important)
            || e.PropertyName == nameof(TaskItem.IsCompleted))
        {
            TasksCollection.Refresh();
        }

        if (e.PropertyName == nameof(TaskItem.DueDate))
        {
            if(sender is TaskItem task)
            {
                if (task.isToday() && !TodayTasks.Contains(task))
                    TodayTasks.Add(task);


                if(!task.isToday() && TodayTasks.Contains(task))
                    TodayTasks.Remove(task);

            }
        }
    }

    private void Remove()
    {
        if (Selected == null)
            return;

        var result = System.Windows.MessageBox.Show(Properties.Resources.AskToDel, Properties.Resources.Removal, System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question);

        if (result != System.Windows.MessageBoxResult.Yes)
            return;

        int indeks = Tasks.IndexOf(Selected);

        Selected.PropertyChanged -= Item_PropertyChanged;

        if (TodayTasks.Contains(Selected))
            TodayTasks.Remove(Selected);

        bool flag = _taskItemService.Delete(Selected.Id);
        if(flag)
            Tasks.Remove(Selected);

        if (flag)
        {
            if (Tasks.Count > 0 && Tasks.Count > indeks)
                Selected = Tasks[indeks];
            else
            {
                if (Tasks.Count > 0)
                    Selected = Tasks[Tasks.Count - 1];
                else
                    Selected = Tasks.FirstOrDefault();
            }    
        }
    }

    private void SetAsDone(TaskItem item = null)
    {
        if (item == null)
            item = Selected;

        item.IsCompleted = true;
        _taskItemService.Update(item);
        RaisePropertyChanged(nameof(Selected));
    }
    private void Save()
    {
        if(Selected != null)
            _taskItemService.Update(Selected);
        _taskItemService.Save();
    }

    #endregion
}
