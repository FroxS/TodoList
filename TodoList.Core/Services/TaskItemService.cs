using Microsoft.EntityFrameworkCore;
using TodoList.Core.Contracts;
using TodoList.Core.Models;
using TodoList.Core.Repository;

namespace TodoList.Core.Services
{
    internal class TaskItemService : BaseService<TaskItem, ITaskItemRepository>, ITaskItemService
    {
        #region Private properties

        private ITaskItemRepository _myRepo => _repozitory as ITaskItemRepository;

        #endregion

        #region Public properties

        #endregion

        #region Constructors

        public TaskItemService(ITaskItemRepository repozitory) : base(repozitory)
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
            if (AutoSave)
                _myRepo.Save();
        }

        public void RemoveSubItem(TaskItem item, TaskISubtem child)
        {
            item.Items.Remove(child);
            _myRepo.RemoveSubItem(item, child);
            if (AutoSave)
                _myRepo.Save();
        }


        public void RemoveSubItem(Guid id, Guid subitem_id)
        {
            var found = _myRepo.GetByIdWithItems(id);
            if(found != null && found.Items != null)
            {
                var exit = found.Items.FirstOrDefault(x => x.Id == subitem_id);
                if(exit != null)
                {
                    found.Items.Remove(exit);
                }
            }
            _myRepo.Save();
        }

        public TaskISubtem GetSubItem(Guid id)
        {
            return _myRepo.GetSubItem(id);
        }

        public bool SetAsDone(Guid id)
        {
            var found = _myRepo.GetByIdWithItems(id);
            if (found == null)
                return false;

            found.IsCompleted = true;
            _myRepo.Save();
            return true;
        }
       
        public bool SetChildAsDone(Guid id, Guid subitem_id)
        {
            var found = _myRepo.GetByIdWithItems(id);
            if (found == null)
                return false;


            var subitem = found.Items.FirstOrDefault(x => x.Id == subitem_id);
            if (subitem == null)
                return false;

            subitem.IsCompleted = true;
            _myRepo.Save();
            return true;
            
        }

        #endregion
    }
}