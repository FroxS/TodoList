using MahApps.Metro.Controls;
using System.Globalization;
using System.Windows;

namespace TodoList.Controls
{
    public class DateTimePickerFormat : DateTimePicker
    {
        #region Properties

        public string DateFormat
        {
            get { return (string)GetValue(DateFormatProperty); }
            set { SetValue(DateFormatProperty, value); }
        }

        #endregion

        #region Dependecy

        public static readonly DependencyProperty DateFormatProperty =
            DependencyProperty.Register(nameof(DateFormat), typeof(string), typeof(DateTimePickerFormat), new PropertyMetadata("yyyy-MM-dd HH:mm Z"));

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public DateTimePickerFormat()
        {

        }

        #endregion

        #region Methods

        protected override string GetValueForTextBox()
        {
            DateTimeFormatInfo dateTimeFormat = base.SpecificCultureInfo.DateTimeFormat;
            return GetSelectedDateTimeFromGUI()?.ToString(DateFormat, Thread.CurrentThread.CurrentUICulture);
        }

        private DateTime? GetSelectedDateTimeFromGUI()
        {
            DateTime? selectedDateTime = base.SelectedDateTime;
            if (selectedDateTime.HasValue)
            {
                return selectedDateTime.Value.Date + GetSelectedTimeFromGUI().GetValueOrDefault();
            }

            return null;
        }

        #endregion
    }
}