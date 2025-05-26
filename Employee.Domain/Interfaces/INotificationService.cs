namespace Employee.Domain.Interfaces;

public interface INotificationService
{
    bool HasNotifications();
    List<string> GetNotifications();
    void AddNotification(string notification);
    public void ClearNotifications();
}