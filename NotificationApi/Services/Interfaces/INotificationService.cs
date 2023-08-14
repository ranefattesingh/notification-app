using NotificationApi.Services.Models;

namespace NotificationApi.Services.Interfaces;

public interface INotificationService {
    Task<Guid> CreateNotification(NotificationServiceModel model);
    NotificationServiceModel? GetNotification(Guid id);
    IEnumerable<NotificationServiceModel> GetNotificationList();
}