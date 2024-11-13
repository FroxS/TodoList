using System.ComponentModel.DataAnnotations;

namespace TodoList.Core.Models
{
    public class BaseItem : ObservableObject
    {
        #region Public properties

        /// <summary>
        /// Unique identifier for the task.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseItem()
        {

        }

        #endregion
    }
}