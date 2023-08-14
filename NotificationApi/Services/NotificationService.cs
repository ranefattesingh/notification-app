using NotificationApi.Services.Models;
using NotificationApi.Services.Interfaces;
using NotificationApi.Repositories.Interfaces;
using NotificationApi.Repositories.Models;
using Microsoft.AspNetCore.SignalR;
using NotificationApi.SignalRHub;

namespace NotificationApi.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly INotifierService _notifierService;
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(
        INotificationRepository notificationRepository,
        INotifierService notifierService,
        ILogger<NotificationService> logger
    ) {
        _notificationRepository = notificationRepository;
        _notifierService = notifierService;
        _logger = logger;
    }

    public async Task<Guid> CreateNotification(NotificationServiceModel serviceModel)
    {
        var repositoryModel = new NotificationRepositoryModel{
            Title = serviceModel.Title,
            Description = serviceModel.Description,
        };

        serviceModel.ID = _notificationRepository.CreateNotification(repositoryModel);
        await _notifierService.SendNotifications(serviceModel);


        return serviceModel.ID;
    }

    public NotificationServiceModel? GetNotification(Guid id)
    {
        NotificationServiceModel? serviceModel = null;
        
        var repositoryModel = _notificationRepository.GetNotification(id);
        if (repositoryModel != null)
        {
            serviceModel = new NotificationServiceModel
            {
                ID = repositoryModel.ID,
                Title = repositoryModel.Title,
                Description = repositoryModel.Description,
            };
        }

        return serviceModel;
    }

    public IEnumerable<NotificationServiceModel> GetNotificationList()
    {
        var repositoryModels = _notificationRepository.GetNotificationList();

        return repositoryModels.Select(repositoryModel => new NotificationServiceModel{
            ID = repositoryModel.ID,
            Title = repositoryModel.Title,
            Description = repositoryModel.Description,
        }).ToList();
    }
}