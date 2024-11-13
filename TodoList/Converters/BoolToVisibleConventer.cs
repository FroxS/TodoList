using System.Globalization;
using System.Windows;

namespace TodoList.Converters
{
    public class BoolToVisibleConventer : BaseValueConventer<BoolToVisibleConventer>
    {
        #region Conventers

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool flag)
            {
                if(parameter == null)
                    return flag ? Visibility.Visible : Visibility.Collapsed;
                else
                    return flag ? Visibility.Collapsed : Visibility.Visible;
            }

            if(value == null || (value is string str && string.IsNullOrEmpty(str)))
                return parameter == null ? Visibility.Collapsed : Visibility.Visible;

            return parameter == null ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}