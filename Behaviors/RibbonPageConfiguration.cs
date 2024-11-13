using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;

using Fluent;

using Prism.Mvvm;

namespace TodoList.Behaviors;

public class RibbonPageConfiguration
{
    #region Properties

    public Collection<RibbonGroupBox> HomeGroups { get; set; } = new Collection<RibbonGroupBox>();

    public Collection<RibbonTabItem> Tabs { get; set; } = new Collection<RibbonTabItem>();

    #endregion

    #region Constructor

    public RibbonPageConfiguration()
    {
    }

    #endregion

    #region Methods

    public void SetDataContext(BindableBase viewModel, BindingMode bindingMode = BindingMode.OneWay)
    {
        foreach (var groups in HomeGroups)
        {
            groups.SetBinding(FrameworkElement.DataContextProperty, new Binding
            {
                Source = viewModel,
                Mode = bindingMode
            });
        }

        foreach (var tab in Tabs)
        {
            tab.SetBinding(FrameworkElement.DataContextProperty, new Binding
            {
                Source = viewModel,
                Mode = bindingMode
            });
        }
    }

    #endregion
}
