using TodoList.Contracts.Services;
using TodoList.Delegates;
using TodoList.Models;

namespace TodoList.ViewModels
{
    public class MessagesViewModel : BaseViewModel, IMessagesViewModel
    {
        #region Properties

        private string _message;

        private EMessageType _type;

        #endregion

        #region Properties

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public EMessageType Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }

        #endregion

        #region Events

        public event MessageDelegate OnSetMessage;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public MessagesViewModel()
        {

        }

        #endregion

        #region Methods

        public void SetMessage(string message)
        {
            Type = EMessageType.Message;
            Message = message;
            OnSetMessage?.Invoke(Message, Type);
        }

        public void SetWarning(string message)
        {
            Type = EMessageType.Warning;
            Message = message;
            OnSetMessage?.Invoke(Message, Type);
        }

        public void SetError(string message)
        {
            Type = EMessageType.Error;
            Message = message;
            OnSetMessage?.Invoke(Message, Type);
        }

        public void Clear()
        {
            Message = null;
            OnSetMessage?.Invoke(null, Type);
        }

        #endregion
    }
}