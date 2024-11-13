using Microsoft.EntityFrameworkCore;
using TodoList.Core.Contracts;
using TodoList.Core.Models;
using TodoList.Core.Repository;

namespace TodoList.Core.Services
{
    internal class TaskItemService : BaseService<TaskItem, TaskItemRepository>, ITaskItemService
    {
        #region Private properties

        private TaskItemRepository _myRepo => _repozitory as TaskItemRepository;

        #endregion

        #region Public properties

        #endregion

        #region Constructors

        public TaskItemService(TaskItemRepository repozitory) : base(repozitory)
        {
        }

        #endregion

        #region Methods

        public List<TaskItem> GetTasksForToday()
        {
            DateTime today = DateTime.Now;
            return GetAll().Where(x =>
            !x.IsCompleted 
            && x.isToday()
            ).ToList();
        }

        public void AddSubItem(TaskItem item, TaskISubtem child)
        {
            child.IdParent = item.Id;
            child.Parent = item;
            if (item.Items == null)
                item.Items = new System.Collections.ObjectModel.ObservableCollection<TaskISubtem>();
            item.Items.Add(child);
            _myRepo.AddSubItem(item,child);
            _myRepo.Save();
        }

        public void RemoveSubItem(TaskItem item, TaskISubtem child)
        {
            item.Items.Remove(child);
            _myRepo.RemoveSubItem(item, child);
            _myRepo.Save();
        }

        #endregion
    }
}