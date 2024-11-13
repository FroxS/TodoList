using System.Globalization;
using System.Windows.Data;

namespace TodoList.Converters;

public class GroupToNameConventer : BaseValueConventer<GroupToNameConventer>
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is CollectionViewGroup group)
            return $"{group.Name} ({group.ItemCount})";
        return value;
    }

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

}

