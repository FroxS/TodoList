using TodoList.Core.Models;
using Windows.UI.Notifications;

namespace TodoList.Contracts.Services;

public interface ITodayTaksNotificationsService
{
    void SetItem(TaskItem item);
    public abstract void ShowToastNotification(ToastNotification toastNotification);

    public abstract void ShowToastNotificationSample();
}
