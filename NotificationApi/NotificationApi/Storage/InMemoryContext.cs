using Microsoft.EntityFrameworkCore;
using NotificationApi.Storage.Interfaces;
using NotificationApi.Storage.Models;

namespace NotificationApi.Storage;

public class InMemoryContext : DbContext, IStorage {
    private DbSet<Notification> _notifications;

    public InMemoryContext(DbContextOptions options) : base(options) {
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
        return _notifications.Select(x => new Notification
        {
            ID = x.ID,
            Title = x.Title,
            Description = x.Description,
        }).ToList();
    }
}