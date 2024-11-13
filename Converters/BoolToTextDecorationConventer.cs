using System.Globalization;
using System.Windows;

namespace TodoList.Converters
{
    public class BoolToTextDecorationConventer : BaseValueConventer<BoolToTextDecorationConventer>
    {
        #region Conventers

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool flag)
            {
                if(parameter == null)
                    return flag ? null : TextDecorations.Strikethrough;
                else
                    return flag ? TextDecorations.Strikethrough : null;
            }

            if(value == null)
                return TextDecorations.Strikethrough;

            return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}