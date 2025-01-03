﻿using System.Windows.Controls;

using MahApps.Metro.Controls;

using Prism.Regions;

using TodoList.Constants;
using TodoList.Contracts.Services;

namespace TodoList.Services;

public class RightPaneService : IRightPaneService
{
    #region Fields
    private readonly IRegionManager _regionManager;
    private IRegionNavigationService _rightPaneNavigationService;
    private SplitView _splitView;

    #endregion

    #region Events

    public event EventHandler PaneOpened;

    public event EventHandler PaneClosed;

    #endregion

    #region Constructor

    public RightPaneService(IRegionManager regionManager)
    {
        _regionManager = regionManager;
    }

    #endregion

    #region Methods

    public void Initialize(SplitView splitView, ContentControl rightPaneContentControl)
    {
        _splitView = splitView;
        RegionManager.SetRegionName(rightPaneContentControl, Regions.RightPane);
        RegionManager.SetRegionManager(rightPaneContentControl, _regionManager);
        _rightPaneNavigationService = _regionManager.Regions[Regions.RightPane].NavigationService;
        _splitView.PaneClosed += OnPaneClosed;
    }

    public void CleanUp()
    {
        _splitView.PaneClosed -= OnPaneClosed;
        _regionManager.Regions.Remove(Regions.RightPane);
    }

    public void OpenInRightPane(string pageKey, NavigationParameters navigationParameters = null)
    {
        if (_rightPaneNavigationService.CanNavigate(pageKey))
        {
            _rightPaneNavigationService.RequestNavigate(pageKey, navigationParameters);
        }

        _splitView.IsPaneOpen = true;
        PaneOpened?.Invoke(_splitView, EventArgs.Empty);
    }

    private void OnPaneClosed(object sender, EventArgs e)
       => PaneClosed?.Invoke(sender, e);

    #endregion
}
