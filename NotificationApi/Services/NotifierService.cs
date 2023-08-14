using Microsoft.AspNetCore.SignalR;
using NotificationApi.Services.Interfaces;
using NotificationApi.Services.Models;
using NotificationApi.SignalRHub;

namespace NotificationApi.Services;

public class NotifierService : INotifierService {
    private readonly ILogger<NotifierService> _logger;
    private readonly IHubContext<NotificationHub> _notificationHub;

    public NotifierService(
        IHubContext<NotificationHub> notificationHub,
        ILogger<NotifierService> logger
    ) {
        _notificationHub = notificationHub;
        _logger = logger;
    }

    public async Task SendNotifications(NotificationServiceModel model)
    {
        try
        {
            await _notificationHub.
                        Clients.
                        All.
                        SendAsync("NotificationCenter", model);

            _logger.LogInformation(string.Format("notification sent successful with notificationID = {0}", model.ID));
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "fail to send notifications");

            throw new Exception("fail to send notification: " + ex.Message, ex);
        }
    }
}