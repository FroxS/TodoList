using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

using CommunityToolkit.WinUI.Notifications;

using Microsoft.Extensions.Configuration;

using Prism.Ioc;
using Prism.Unity;

using TodoList.Constants;
using TodoList.Contracts.Services;
using TodoList.Core.Contracts;
using TodoList.Core.EF;
using TodoList.Core.Models;
using TodoList.Core.Repository;
using TodoList.Core.Services;
using TodoList.Models;
using TodoList.Services;
using TodoList.ViewModels;
using TodoList.Views;

namespace TodoList;

public partial class App : PrismApplication
{
    public const string ToastNotificationActivationArguments = "ToastNotificationActivationArguments";

    private string[] _startUpArgs;
    private static SplashScreenWindow _splashScreenWindow;
    private static Thread _splashThread;
    public App()
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
        var appLocation = Path.GetDirectoryName(AppContext.BaseDirectory);
        var conf = new ConfigurationBuilder()
            .SetBasePath(appLocation)
            .AddJsonFile("appsettings.json")
            .Build();

        var serv_file = new FileService();

        var dd = new PersistAndRestoreService(serv_file, conf.GetSection(nameof(AppConfig))
            .Get<AppConfig>());
        dd.RestoreData();
        if (Properties["Lang"] != null)
        {

            string lang = Properties["Lang"].ToString();
            SetCulture(lang);
        }
        else
        {
            CultureInfo systemCulture = CultureInfo.InstalledUICulture;
            if (string.IsNullOrEmpty(systemCulture?.Name))
            {
                string lang = "";
                Properties["Lang"] = lang = systemCulture.Name;
                SetCulture(lang);
            }
        }

    }

    protected override Window CreateShell()
        => Container.Resolve<ShellWindow>();

    protected override void InitializeModules()
    {
        _splashThread = new Thread(() =>
        {
            _splashScreenWindow = new SplashScreenWindow();
            _splashScreenWindow.Show();
            System.Windows.Threading.Dispatcher.Run();
        });

        _splashThread.SetApartmentState(ApartmentState.STA);
        _splashThread.Start();
        base.InitializeModules();
    }

    internal static void CloseSplashScreen()
    {
        if (_splashScreenWindow != null)
        {
            _splashScreenWindow.Dispatcher.Invoke(() =>
            {
                _splashScreenWindow.Close();
            });
            _splashScreenWindow.Dispatcher.InvokeShutdown();
        }
    }

    protected override async void OnInitialized()
    {
        var persistAndRestoreService = Container.Resolve<IPersistAndRestoreService>();
        //persistAndRestoreService.RestoreData();

        var themeSelectorService = Container.Resolve<IThemeSelectorService>();
        themeSelectorService.InitializeTheme();

        ToastNotificationManagerCompat.OnActivated += (toastArgs) =>
        {
            Current.Dispatcher.Invoke(async () =>
            {
                var config = Container.Resolve<IConfiguration>();
                config[App.ToastNotificationActivationArguments] = toastArgs.Argument;
                App.Current.MainWindow.Show();
                App.Current.MainWindow.Activate();
                if (App.Current.MainWindow.WindowState == WindowState.Minimized)
                {
                    App.Current.MainWindow.WindowState = WindowState.Normal;
                }

                await Task.CompletedTask;
            });
        };
        var tasks = Container.Resolve<ITaskItemService>().GetTasksForToday();
        var taskNotificationsService = Container.Resolve<ITodayTaksNotificationsService>();
        if (tasks != null)
        {
            foreach(TaskItem task in tasks)
            {
                if (task.AddNotification)
                {
                    taskNotificationsService.SetItem(task);
                    taskNotificationsService.ShowToastNotificationSample();
                }
                
            }
        }

        if (ToastNotificationManagerCompat.WasCurrentProcessToastActivated())
        {
            return;
        }

        base.OnInitialized();
        await Task.CompletedTask;
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _startUpArgs = e.Args;
        base.OnStartup(e);
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // Core Services
        containerRegistry.Register<IFileService, FileService>();
        containerRegistry.Register<ITaskItemService, TaskItemService>();

        // Database
        containerRegistry.Register<TodoListBaseDBContext>();

        // Repository
        containerRegistry.Register<ITaskItemRepository, TaskItemRepository>();

        // App Services
        containerRegistry.RegisterSingleton<ITodayTaksNotificationsService, TodayTaksNotificationsService>();
        containerRegistry.Register<IApplicationInfoService, ApplicationInfoService>();
        containerRegistry.Register<IPersistAndRestoreService, PersistAndRestoreService>();
        containerRegistry.Register<IThemeSelectorService, ThemeSelectorService>();
        containerRegistry.RegisterSingleton<IRightPaneService, RightPaneService>();

        // Views
        containerRegistry.RegisterForNavigation<SettingsPage, SettingsViewModel>(PageKeys.Settings);
        containerRegistry.RegisterForNavigation<MainPage, MainViewModel>(PageKeys.Main);
        containerRegistry.RegisterForNavigation<ShellWindow, ShellViewModel>();

        // Messages
        containerRegistry.RegisterSingleton<IMessagesViewModel, MessagesViewModel>();

        // Configuration
        var configuration = BuildConfiguration();
        var appConfig = configuration
            .GetSection(nameof(AppConfig))
            .Get<AppConfig>();

        // Register configurations to IoC
        containerRegistry.RegisterInstance<IConfiguration>(configuration);
        containerRegistry.RegisterInstance<AppConfig>(appConfig);

    }

    private IConfiguration BuildConfiguration()
    {
        var activationArgs = new Dictionary<string, string>
        {
            { ToastNotificationActivationArguments, string.Empty }
        };

        var appLocation = Path.GetDirectoryName(AppContext.BaseDirectory);
        return new ConfigurationBuilder()
            .SetBasePath(appLocation)
            .AddJsonFile("appsettings.json")
            .AddCommandLine(_startUpArgs)
            .AddInMemoryCollection(activationArgs)
            .Build();
    }

    private void OnExit(object sender, ExitEventArgs e)
    {
        var persistAndRestoreService = Container.Resolve<IPersistAndRestoreService>();
        persistAndRestoreService.PersistData();
    }

    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        MessageBox.Show(e.Exception.Message);
    }

    public static void ReloadResources()
    {
        //var oldDict = Application.Current.Resources.MergedDictionaries.FirstOrDefault();
        //if (oldDict != null)
        //{
        //    Application.Current.Resources.MergedDictionaries.Remove(oldDict);
        //}
        //
        //var newDict = new ResourceDictionary
        //{
        //    Source = new Uri($"Resources.{Thread.CurrentThread.CurrentUICulture.Name}.resx", UriKind.Relative)
        //};
        //Application.Current.Resources.MergedDictionaries.Add(newDict);
    }

    public void SetCulture(string lang)
    {
        CultureInfo cultureInfo = string.IsNullOrEmpty(lang)
            ? CultureInfo.CurrentUICulture
            : new CultureInfo(lang);

        Thread.CurrentThread.CurrentCulture = cultureInfo;
        Thread.CurrentThread.CurrentUICulture = cultureInfo;

        // Ustawienie kultury dla całej aplikacji
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
    }
}
