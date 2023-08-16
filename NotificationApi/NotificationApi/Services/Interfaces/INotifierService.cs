using NotificationApi.Services.Models;

namespace NotificationApi.Services.Interfaces;

public interface INotifierService {
    Task SendNotifications(NotificationServiceModel model);
}