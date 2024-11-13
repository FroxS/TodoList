using System.Windows;
using System.Windows.Controls;
using TodoList.ViewModels;

namespace TodoList.Views;

public partial class MainPage : UserControl
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        if(DataContext is MainViewModel vm)
        {
            vm.AddSubItemCommand.Invoke(null);
        }
    }
    private void Remove_subitem_click(object sender, System.Windows.RoutedEventArgs e)
    {
        if (DataContext is MainViewModel vm && sender is FrameworkElement fe)
        {
            vm.RemoveSubItemCommand.Invoke(fe?.DataContext);
        }
    }

    private void CalendarControl_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
    {
        if (CalendarControl.SelectedDate.HasValue)
        {
            if (DataContext is MainViewModel vm)
            {
                vm.SearchDate = CalendarControl.SelectedDate;
            }
            CalendarPopup.IsOpen = false;
        }
    }

    private void CalendarButton_Click(object sender, RoutedEventArgs e)
    {
        CalendarPopup.IsOpen = true;
    }
}
