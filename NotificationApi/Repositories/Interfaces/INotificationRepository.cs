namespace NotificationApi.Repositories.Interfaces;

using NotificationApi.Repositories.Models;

public interface INotificationRepository {
    Guid CreateNotification(NotificationRepositoryModel model);
    NotificationRepositoryModel? GetNotification(Guid id);
    IEnumerable<NotificationRepositoryModel> GetNotificationList();
}