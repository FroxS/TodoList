namespace TodoList.Core.Models
{
    public class TaskItem : BaseItem
    {
        #region Fields

        private string _title;
        private string _description;
        private string _symbol;
        private DateTime _dueDate;
        private bool _isCompleted;
        private bool _important;
        private string _group;
        private bool _addNotification;

        #endregion

        #region Properties

        public virtual string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(); }
        }

        public virtual string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
        }

        public virtual string Symbol
        {
            get => _symbol;
            set { _symbol = value; OnPropertyChanged(); }
        }

        public virtual DateTime DueDate
        {
            get => _dueDate;
            set { _dueDate = value; OnPropertyChanged(); }
        }

        public virtual bool IsCompleted
        {
            get => _isCompleted;
            set { _isCompleted = value; OnPropertyChanged(); }
        }

        public virtual bool Important
        {
            get => _important;
            set { _important = value; OnPropertyChanged(); }
        }

        public virtual string Group
        {
            get => _group;
            set { _group = value; OnPropertyChanged(); }
        }

        public virtual bool AddNotification
        {
            get => _addNotification;
            set { _addNotification = value; OnPropertyChanged(); }
        }

        #endregion

        #region Constructors

        public TaskItem()
        {

        }

        #endregion

        #region Methods

        public bool isToday()
        {
            if (DueDate == DateTime.Today)
                return true;


            return false;
        }

        #endregion
    }
}