using TodoList.Core.Models;

namespace TodoList.Core.Contracts
{
    public interface ITaskItemRepository : IBaseRepository<TaskItem>
    {
        void AddSubItem(TaskItem item, TaskISubtem child);
        void RemoveSubItem(TaskItem item, TaskISubtem child);
    }
}