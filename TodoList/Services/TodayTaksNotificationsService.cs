using CommunityToolkit.WinUI.Notifications;

using TodoList.Contracts.Services;
using TodoList.Core.Models;
using Windows.UI.Notifications;

namespace TodoList.Services;

public partial class TodayTaksNotificationsService : ITodayTaksNotificationsService
{
    #region Fields

    private TaskItem _item;

    #endregion

    #region Constructor

    public TodayTaksNotificationsService()
    {
    }

    #endregion

    #region Methods

    public void SetItem(TaskItem item)
    {
        _item = item;
    }

    public void ShowToastNotification(ToastNotification toastNotification)
    {
        ToastNotificationManagerCompat.CreateToastNotifier().Show(toastNotification);
    }

    #endregion
}
