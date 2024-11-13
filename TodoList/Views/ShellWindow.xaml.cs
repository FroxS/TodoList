using System.Globalization;
using System.Windows;

using Fluent;

using MahApps.Metro.Controls;

using Prism.Regions;

using TodoList.Constants;
using TodoList.Contracts.Services;

namespace TodoList.Views;

public partial class ShellWindow : MetroWindow, IRibbonWindow
{
    public RibbonTitleBar TitleBar
    {
        get => (RibbonTitleBar)GetValue(TitleBarProperty);
        private set => SetValue(TitleBarPropertyKey, value);
    }

    private static readonly DependencyPropertyKey TitleBarPropertyKey = DependencyProperty.RegisterReadOnly(nameof(TitleBar), typeof(RibbonTitleBar), typeof(ShellWindow), new PropertyMetadata());

    public static readonly DependencyProperty TitleBarProperty = TitleBarPropertyKey.DependencyProperty;

    public ShellWindow(IRegionManager regionManager, IRightPaneService rightPaneService)
    {
        InitializeComponent();
        RegionManager.SetRegionName(menuContentControl, Regions.Main);
        RegionManager.SetRegionManager(menuContentControl, regionManager);
        rightPaneService.Initialize(splitView, rightPaneContentControl);
        navigationBehavior.Initialize(regionManager);
        tabsBehavior.Initialize(regionManager);
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        App.Current.Dispatcher.Invoke(() => {
            if (App.Current.Properties["Lang"] != null)
            {

               string lang = App.Current.Properties["Lang"].ToString();
                CultureInfo cultureInfo = string.IsNullOrEmpty(lang)
            ? CultureInfo.CurrentUICulture
            : new CultureInfo(lang);

                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;

                // Ustawienie kultury dla całej aplikacji
                CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
                CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
                App.ReloadResources();
            }
        });

        App.CloseSplashScreen(); 
        var window = sender as MetroWindow;
        window.Activate();
        TitleBar = window.FindChild<RibbonTitleBar>("RibbonTitleBar");
        TitleBar.InvalidateArrange();
        TitleBar.UpdateLayout();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        tabsBehavior.Unsubscribe();
    }
}
