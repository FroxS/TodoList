using TodoList.Core.Models;

namespace TodoList.Core.Contracts
{
    public interface ITaskItemService : IBaseService<ITaskItemRepository, TaskItem>
    {
        List<TaskItem> GetTasksForToday();
    }
}