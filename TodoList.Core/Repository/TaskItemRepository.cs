using TodoList.Core.Contracts;
using TodoList.Core.EF;
using TodoList.Core.Models;

namespace TodoList.Core.Repository
{
    internal class TaskItemRepository : BaseRepository<TaskItem>, ITaskItemRepository
    {
        #region Private properties

        #endregion

        #region Public properties

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public TaskItemRepository(TodoListBaseDBContext dbContext) :base(dbContext)
        {

        }

        #endregion
    }
}