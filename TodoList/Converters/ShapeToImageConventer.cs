using System.Globalization;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TodoList.Converters
{
    public class ShapeToImageConventer : BaseValueConventer<ShapeToImageConventer>
    {
        #region Conventers

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is Path path)
            {
                var bounds = path.Data.GetRenderBounds(null);
                path.Measure(bounds.Size);
                path.Arrange(bounds);
                var bitmap = new RenderTargetBitmap(
                    (int)bounds.Width, (int)bounds.Height, 96, 96, PixelFormats.Pbgra32);
                bitmap.Render(path);
                return bitmap;
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