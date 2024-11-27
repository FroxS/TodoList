using TodoList.Core.Models;

namespace TodoList.Core.Contracts
{
    public interface ITaskItemService : IBaseService<ITaskItemRepository, TaskItem>
    {
        List<TaskItem> GetTasksForToday();
        void AddSubItem(TaskItem item, TaskISubtem child);
        void RemoveSubItem(TaskItem item, TaskISubtem child);
        TaskISubtem GetSubItem(Guid id);
        void RemoveSubItem(Guid id, Guid subitem_id);
        bool SetAsDone(Guid id );
        bool SetChildAsDone(Guid id, Guid subitem_id);
    }
}