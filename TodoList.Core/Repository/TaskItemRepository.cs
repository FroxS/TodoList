using Microsoft.EntityFrameworkCore;
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

        #region Method

        public void AddSubItem(TaskItem item, TaskISubtem child)
        {
            DbContext.SubTasks.Add(child);
        }

        public void RemoveSubItem(TaskItem item, TaskISubtem child)
        {
            DbContext.SubTasks.Remove(child);
        }

        public override List<TaskItem> GetAll()
        {
            return DbContext.Tasks.Include(x => x.Items).ToList();
        }

        public override async Task<List<TaskItem>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await DbContext.Tasks.Include(x => x.Items).ToListAsync();
        }


        #endregion
    }
}