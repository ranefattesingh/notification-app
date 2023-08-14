using NotificationApi.Storage.Interfaces;
using NotificationApi.Storage.Models;

namespace NotificationApi.Storage;

public class InMemory : IStorage {
    private List<Notification> _notifications;

    public InMemory() {
        _notifications = new List<Notification>();
    }

    public Guid Add(Notification notification) 
    {
        var nextID = Guid.NewGuid();
        notification.ID = nextID;
        _notifications.Add(notification);

        return nextID;
    }

    public Notification? Get(Guid id)
    {
        return _notifications.FirstOrDefault(x => x.ID == id);
    }

    public List<Notification> GetAll()
    {
        return _notifications;
    }
}