using System.Globalization;
using System.Windows;
using System.Windows.Input;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

using TodoList.Contracts.Services;
using TodoList.Models;

namespace TodoList.ViewModels;

public class SettingsViewModel : BindableBase, INavigationAware
{
    #region Fields

    private readonly AppConfig _appConfig;
    private readonly IThemeSelectorService _themeSelectorService;
    private readonly IApplicationInfoService _applicationInfoService;
    private AppTheme _theme;
    private string _versionDescription;
    private Languages _langs = new Languages();
    private Lang _lang;
    #endregion

    #region Properties

    public AppTheme Theme
    {
        get { return _theme; }
        set { SetProperty(ref _theme, value); }
    }

    public string VersionDescription
    {
        get { return _versionDescription; }
        set { SetProperty(ref _versionDescription, value); }
    }

    public Languages Langs
    {
        get { return _langs; }
        set { SetProperty(ref _langs, value); }
    }
    public Lang SelectedLang
    {
        get { return _lang; }
        set { SetProperty(ref _lang, value); SetLang(); }
    }

    #endregion

    #region Commands

    public ICommand SetThemeCommand { get; }

    #endregion

    #region Constructor

    public SettingsViewModel(AppConfig appConfig, IThemeSelectorService themeSelectorService, IApplicationInfoService applicationInfoService)
    {
        _appConfig = appConfig;
        _themeSelectorService = themeSelectorService;
        _applicationInfoService = applicationInfoService;
        SetThemeCommand = new DelegateCommand<string>(OnSetTheme);
        SelectedLang = Langs.Find(Thread.CurrentThread.CurrentUICulture);
    }

    #endregion

    #region Navigation

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        VersionDescription = $"{Properties.Resources.AppDisplayName} - {_applicationInfoService.GetVersion()}";
        Theme = _themeSelectorService.GetCurrentTheme();
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
       => true;

    #endregion

    #region Command method

    private void OnSetTheme(string themeName)
    {
        var theme = (AppTheme)Enum.Parse(typeof(AppTheme), themeName);
        _themeSelectorService.SetTheme(theme);
    }

    private void SetLang()
    {
        if (SelectedLang == null)
            return;
        var culture = Thread.CurrentThread.CurrentUICulture;
        if ((culture != null && culture.Name != SelectedLang.Key) || culture == null)
        {

            string lang = SelectedLang.Key;
            CultureInfo cultureInfo = string.IsNullOrEmpty(lang)
            ? CultureInfo.CurrentUICulture
            : new CultureInfo(lang);

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            App.ReloadResources();

            var result = MessageBox.Show(Properties.Resources.AskRestartApp, Properties.Resources.Restart, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(result == MessageBoxResult.Yes)
            {
                App.Current.Properties["Lang"] = SelectedLang.Key;
                string exe = _applicationInfoService.GetExePath();
                System.Diagnostics.Process.Start(exe);
                Application.Current.Shutdown();
            }

        }
        App.Current.Properties["Lang"] = SelectedLang.Key;
    }

    #endregion
}
