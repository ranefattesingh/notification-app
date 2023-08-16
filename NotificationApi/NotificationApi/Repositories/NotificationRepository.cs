using NotificationApi.Repositories.Interfaces;
using NotificationApi.Repositories.Models;
using NotificationApi.Storage.Interfaces;
using NotificationApi.Storage.Models;

namespace NotificationApi.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly IStorage _storage;
    private readonly ILogger<NotificationRepository> _logger;

    public NotificationRepository(IStorage storage, ILogger<NotificationRepository> logger)
    {
        _storage = storage;
        _logger =  logger;
    }

    public Guid CreateNotification(NotificationRepositoryModel model)
    {
        var notification = new Notification
        {
            Title = model.Title,
            Description = model.Description,
        };

        return _storage.Add(notification);
    }

    public NotificationRepositoryModel? GetNotification(Guid id)
    {
        NotificationRepositoryModel? repositoryModel = null;
        
        var notification = _storage.Get(id);
        if (notification != null)
        {
            repositoryModel = new NotificationRepositoryModel {
                ID = notification.ID,
                Title = notification.Title,
                Description = notification.Description,
            };
        }

        return repositoryModel;
    }

    public IEnumerable<NotificationRepositoryModel> GetNotificationList()
    {
        IEnumerable<NotificationRepositoryModel> result = new List<NotificationRepositoryModel>();

        var notifications = _storage.GetAll();
        if (notifications != null)
        {
            result = notifications.Select(notification => new NotificationRepositoryModel{
                ID = notification.ID,
                Title = notification.Title,
                Description = notification.Description,
            }).ToList();

        }

        return result;
    }
}