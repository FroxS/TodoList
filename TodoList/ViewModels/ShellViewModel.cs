using System.Globalization;
using System.Windows.Input;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

using TodoList.Constants;
using TodoList.Contracts.Services;
using TodoList.Services;

namespace TodoList.ViewModels;

public class ShellViewModel : BaseViewModel
{
    #region Fields

    private readonly IRegionManager _regionManager;
    private readonly IRightPaneService _rightPaneService;
    private IRegionNavigationService _navigationService;

    #endregion

    #region Commands

    public IMessagesViewModel Message { get; }

    #endregion

    #region Commands

    public ICommand LoadedCommand { get; }

    public ICommand UnloadedCommand { get; }

    #endregion

    #region Constructor

    public ShellViewModel(IRegionManager regionManager, IRightPaneService rightPaneService, IMessagesViewModel messageService)
    {
        DelegateCommandEx.ExeptionArgs += DelegateCommandEx_ExeptionArgs;
        _regionManager = regionManager;
        _rightPaneService = rightPaneService;
        Message = messageService;
        LoadedCommand = new DelegateCommandEx(OnLoaded);
        UnloadedCommand = new DelegateCommandEx(OnUnloaded);
    }


    private void DelegateCommandEx_ExeptionArgs(Exception ex)
    {
        Message?.SetError(ex.Message);
    }

    #endregion

    #region Load

    private void OnLoaded()
    {
        _navigationService = _regionManager.Regions[Regions.Main].NavigationService;
        _navigationService.RequestNavigate(PageKeys.Main);
    }

    private void OnUnloaded()
    {
        _regionManager.Regions.Remove(Regions.Main);
        _rightPaneService.CleanUp();
    }

    #endregion
}
