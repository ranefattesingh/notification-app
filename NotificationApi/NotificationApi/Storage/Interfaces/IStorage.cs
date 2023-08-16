using NotificationApi.Storage.Models;

namespace NotificationApi.Storage.Interfaces;

public interface IStorage {
    Guid Add(Notification notification);

    Notification? Get(Guid id);

    List<Notification> GetAll();
}