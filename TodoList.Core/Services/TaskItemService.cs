using TodoList.Core.Contracts;
using TodoList.Core.Models;
using TodoList.Core.Repository;

namespace TodoList.Core.Services
{
    internal class TaskItemService : BaseService<TaskItem, TaskItemRepository>, ITaskItemService
    {
        #region Private properties

        #endregion

        #region Public properties

        #endregion

        #region Constructors

        public TaskItemService(TaskItemRepository repozitory) : base(repozitory)
        {
        }

        #endregion

        #region methods

        public List<TaskItem> GetTasksForToday()
        {
            DateTime today = DateTime.Now;
            return GetAll().Where(x =>
            !x.IsCompleted 
            && x.isToday()
            ).ToList();
        }

        #endregion
    }
}