namespace TodoList.Core.Models
{
    public class TaskISubtem : BaseItem
    {
        #region Fields

        private string _name;
        private bool _isCompleted;
        private Guid _idParent;
        private TaskItem _parent;
        #endregion

        #region Properties

        public virtual string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public virtual Guid IdParent
        {
            get => _idParent;
            set { _idParent = value; OnPropertyChanged(); }
        }

        public virtual bool IsCompleted
        {
            get => _isCompleted;
            set { _isCompleted = value; OnPropertyChanged(); }
        }

        public virtual TaskItem Parent
        {
            get => _parent;
            set { _parent = value; OnPropertyChanged(); }
        }

        #endregion

        #region Constructors

        public TaskISubtem()
        {

        }

        #endregion
    }
}