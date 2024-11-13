using System.Globalization;

namespace TodoList.Converters
{
    public class InverseBoolConventer : BaseValueConventer<InverseBoolConventer>
    {
        #region Conventers

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool flag)
            {
                return !flag;
            }
            return value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}