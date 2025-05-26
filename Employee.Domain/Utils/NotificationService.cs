using Employee.Domain.Interfaces;

namespace Employee.Domain.Utils;

public class NotificationService : INotificationService
{
    private readonly List<string> _notifications;

    public NotificationService()
    {
        _notifications = new List<string>();
    }

    public void AddNotification(string notification)
    {
        _notifications.Add(notification);
    }

    public List<string> GetNotifications()
    {
        return _notifications;
    }

    public bool HasNotifications()
    {
        return _notifications.Count > 0;
    }

    public void ClearNotifications()
    {
        _notifications.Clear();
    }
}