using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TodoList.Controls
{
    public class MyToogle : ToggleButton
    {
        #region Private properties

        #endregion

        #region Properties

        public Geometry CheckedShape
        {
            get { return (Geometry)GetValue(CheckedShapeProperty); }
            set { SetValue(CheckedShapeProperty, value); }
        }

        public Geometry UnCheckedShape
        {
            get { return (Geometry)GetValue(UnCheckedShapeProperty); }
            set { SetValue(UnCheckedShapeProperty, value); }
        }

        #endregion

        #region Dependecy

        public static readonly DependencyProperty CheckedShapeProperty =
            DependencyProperty.Register(nameof(CheckedShape), typeof(Geometry), typeof(MyToogle), new PropertyMetadata(null));

        public static readonly DependencyProperty UnCheckedShapeProperty =
            DependencyProperty.Register(nameof(UnCheckedShape), typeof(Geometry), typeof(MyToogle), new PropertyMetadata(null));

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public MyToogle()
        {
            Checked += UpdateContent;
            Unchecked += UpdateContent;
            Loaded += MyToogle_Loaded;
            Background = Brushes.Transparent;
            BorderThickness = new Thickness(0);

        }

        #endregion

        #region Methods

        private void MyToogle_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateContent(this, null);
        }

        private void UpdateContent(object sender, RoutedEventArgs e)
        {
            Path path = new Path()
            {
                Fill = Foreground,
                Stretch = Stretch.Uniform,
                Data = IsChecked == true ? CheckedShape : UnCheckedShape
            };

            Content = path;
        }

        #endregion
    }
}