using TodoList.Core.Models;

namespace TodoList.Core.Contracts
{
    public interface ITaskItemService : IBaseService<ITaskItemRepository, TaskItem>
    {
        List<TaskItem> GetTasksForToday();
        void AddSubItem(TaskItem item, TaskISubtem child);
        void RemoveSubItem(TaskItem item, TaskISubtem child);
    }
}