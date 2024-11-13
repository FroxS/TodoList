using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

using Fluent;

using Microsoft.Xaml.Behaviors;

using Prism.Regions;

using TodoList.Constants;

namespace TodoList.Behaviors;

public class RibbonTabsBehavior : Behavior<Ribbon>
{
    #region Fields

    private IRegionManager _regionManager;

    #endregion

    #region Dependecy IsHomeTab

    public static readonly DependencyProperty IsHomeTabProperty = DependencyProperty.RegisterAttached(
        "IsHomeTab", typeof(bool), typeof(RibbonTabsBehavior), new PropertyMetadata(default(bool)));

    public static void SetIsHomeTab(DependencyObject element, bool value) => element.SetValue(IsHomeTabProperty, value);

    public static bool GetIsHomeTab(DependencyObject element) => (bool)element.GetValue(IsHomeTabProperty);

    #endregion

    #region Dependecy IsTabFromPage

    public static readonly DependencyProperty IsTabFromPageProperty =
        DependencyProperty.RegisterAttached("IsTabFromPage", typeof(bool), typeof(RibbonTabItem), new PropertyMetadata(false));

    public static bool GetIsTabFromPage(RibbonTabItem item) => (bool)item.GetValue(IsTabFromPageProperty);

    public static void SetIsTabFromPage(RibbonTabItem item, bool value) => item.SetValue(IsTabFromPageProperty, value);

    #endregion

    #region Dependecy IsGroupFromPage

    public static readonly DependencyProperty IsGroupFromPageProperty = DependencyProperty.RegisterAttached("IsGroupFromPage", typeof(bool), typeof(RibbonGroupBox), new PropertyMetadata(false));

    public static bool GetIsGroupFromPage(RibbonGroupBox item) => (bool)item.GetValue(IsGroupFromPageProperty);

    public static void SetIsGroupFromPage(RibbonGroupBox item, bool value) => item.SetValue(IsGroupFromPageProperty, value);

    #endregion

    #region Dependecy PageConfiguration

    public static readonly DependencyProperty PageConfigurationProperty = DependencyProperty.Register("PageConfiguration", typeof(RibbonPageConfiguration), typeof(UserControl), new PropertyMetadata(new RibbonPageConfiguration()));

    public static RibbonPageConfiguration GetPageConfiguration(UserControl item)
        => (RibbonPageConfiguration)item.GetValue(PageConfigurationProperty);
    public static void SetPageConfiguration(UserControl item, RibbonPageConfiguration value)
        => item.SetValue(PageConfigurationProperty, value);

    #endregion

    #region Methods

    public void Initialize(IRegionManager regionManager)
    {
        _regionManager = regionManager;
        var navigationService = _regionManager.Regions[Regions.Main].NavigationService;
        navigationService.Navigated += OnNavigated;
    }

    public void Unsubscribe()
    {
        var navigationService = _regionManager.Regions[Regions.Main].NavigationService;
        navigationService.Navigated -= OnNavigated;
    }

    private void OnNavigated(object sender, RegionNavigationEventArgs e)
    {
        var page = _regionManager.Regions[Regions.Main].ActiveViews.First() as UserControl;
        UpdateTabs(page);
    }

    private void UpdateTabs(UserControl page)
    {
        if (page != null)
        {
            var config = GetPageConfiguration(page);
            SetupHomeGroups(config.HomeGroups);
            SetupTabs(config.Tabs);
        }
    }

    private void SetupHomeGroups(Collection<RibbonGroupBox> homeGroups)
    {
        var homeTab = AssociatedObject.Tabs.FirstOrDefault();
        if (homeTab != null)
        {
            for (int i = homeTab.Groups.Count - 1; i >= 0; i--)
            {
                if (GetIsGroupFromPage(homeTab.Groups[i]))
                {
                    homeTab.Groups.RemoveAt(i);
                }
            }
        }

        foreach (var group in homeGroups)
        {
            if (GetIsGroupFromPage(group))
            {
                homeTab.Groups.Add(group);
            }
        }
    }

    private void SetupTabs(Collection<RibbonTabItem> tabs)
    {
        for (int i = AssociatedObject.Tabs.Count - 1; i >= 0; i--)
        {
            if (GetIsTabFromPage(AssociatedObject.Tabs[i]))
            {
                AssociatedObject.Tabs.RemoveAt(i);
            }
        }

        foreach (var tab in tabs)
        {
            if (GetIsTabFromPage(tab))
            {
                AssociatedObject.Tabs.Add(tab);
            }
        }
    }

    #endregion
}
