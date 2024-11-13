using TodoList.Delegates;

namespace TodoList.Contracts.Services
{
    public interface IMessagesViewModel
    {
        event MessageDelegate OnSetMessage;
        void Clear();
        void SetMessage(string message);
        void SetWarning(string message);
        void SetError(string message);
    }
}