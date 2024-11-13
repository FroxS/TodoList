using CommunityToolkit.WinUI.Notifications;

using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace TodoList.Services;

public partial class TodayTaksNotificationsService
{
    public void ShowToastNotificationSample()
    {
        if (_item == null)
            return;

        var content = new ToastContent()
        {
            Launch = "ToastContentActivationParams",

            Visual = new ToastVisual()
            {
                BindingGeneric = new ToastBindingGeneric()
                {
                    Children =
                    {
                        new AdaptiveText()
                        {
                            Text = "Zadanie na dziś: " + _item.Title,
                        },

                        new AdaptiveText()
                        {
                             Text =_item.Description
                        }
                    }
                }
            },

            Actions = new ToastActionsCustom()
            {
                Buttons =
                {
                    new ToastButton("OK", "ToastButtonActivationArguments")
                    {
                        ActivationType = ToastActivationType.Foreground
                    },
            
                    new ToastButtonDismiss("Cancel")
                }
            }
        };

        var doc = new XmlDocument();
        doc.LoadXml(content.GetContent());
        var toast = new ToastNotification(doc)
        {
            Tag = "Zadanie" + _item.Id,
        };
        ShowToastNotification(toast);
    }
}
