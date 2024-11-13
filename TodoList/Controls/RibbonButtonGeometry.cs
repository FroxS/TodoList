using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace TodoList.Controls;

public class RibbonButtonGeometry : Fluent.Button
{
    #region Private properties

    #endregion

    #region Properties

    public Geometry Shape
    {
        get { return (Geometry)GetValue(ShapeProperty); }
        set { SetValue(ShapeProperty, value); }
    }

    #endregion

    #region Dependecy

    public static readonly DependencyProperty ShapeProperty =
        DependencyProperty.Register(nameof(Shape), typeof(Geometry), typeof(RibbonButtonGeometry), new PropertyMetadata(null));

    #endregion

    #region Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    public RibbonButtonGeometry()
    {
        //System.Windows.Shapes.Path
    }

    #endregion
}

