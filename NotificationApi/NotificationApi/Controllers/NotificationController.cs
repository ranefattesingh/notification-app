using Microsoft.AspNetCore.Mvc;
using NotificationApi.Controllers.Models;
using NotificationApi.Services;
using NotificationApi.Services.Interfaces;
using NotificationApi.Services.Models;

namespace NotificationApi.Controllers;

[ApiController]
// [Route("[controller]")]
[Route("api/v1/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;
    private readonly ILogger<NotificationController> _logger;
    public  NotificationController(
        INotificationService notificationService, 
        ILogger<NotificationController> logger
    ) {
        _notificationService = notificationService;
        _logger = logger;
    }

    [HttpPost(Name = "CreateNotification")]
    public async Task<IActionResult> CreateNotification(NotificationControllerModel model) 
    {
        _logger.LogInformation("CreateNotification", "start");
        
        ObjectResult result;
        
        try
        {
            var serviceModel = new NotificationServiceModel{
                Title = model.Title,
                Description = model.Description,  
            };

            var notificationID = await _notificationService.CreateNotification(serviceModel);

            result = Created("/api/v1/notification", new ControllerResponseModel 
            { 
                Data = new
                {
                    NotificationID = notificationID,
                },
            });
        }
        catch(Exception ex)
        {
            result = StatusCode(500, new ControllerErrorResponseModel{
                Error = string.Format("server couldn't handle the request")
            });

            _logger.LogError(ex, "CreateNotification fail with an exception");
        }

        _logger.LogInformation("CreateNotification", "end");

        return result;
    }


    [HttpGet("{id}", Name = "GetNotificationByID")]
    [Route("{id:int}")]
    public IActionResult GetNotification(Guid id)
    {
        _logger.LogInformation("GetNotificationByID", "start");

        ObjectResult result;

        try
        {
            var serviceModel = _notificationService.GetNotification(id);
            if (serviceModel == null ) 
            {
                result = StatusCode(404, new ControllerErrorResponseModel{
                    Error = string.Format("notification with id = {0} does not exist.", id)
                });
            }
            else
            {
                result = Ok(new ControllerResponseModel{
                    Data = new 
                    {
                        Notification = new NotificationControllerModel{
                            ID = serviceModel.ID,
                            Title = serviceModel.Title,
                            Description = serviceModel.Description,
                        }
                    }
                });
            }
        }
        catch(Exception ex)
        {
            result = StatusCode(500, new ControllerErrorResponseModel{
                Error = string.Format("server couldn't handle the request")
            });

            _logger.LogError(ex, "GetNotificationByID fail with an exception");
        }

        _logger.LogInformation("GetNotificationByID", "end");
        
        return result;
    }

    [HttpGet(Name = "GetNotificationList")]
    [Route("")]
    public IActionResult GetNotificationList()
    {
        _logger.LogInformation("GetNotificationList", "start");

        ObjectResult result;

        try
        {
            var serviceModels = _notificationService.GetNotificationList();
            result = Ok(new ControllerResponseModel{
                Data = new 
                {
                    TotalCount = serviceModels.Count(),
                    Notifications = serviceModels.Select(serviceModel => new NotificationControllerModel{
                        ID = serviceModel.ID,
                        Title = serviceModel.Title,
                        Description = serviceModel.Description,
                    })
                }
            }); 
        }
        catch(Exception ex)
        {
            result = StatusCode(500, new ControllerErrorResponseModel{
                Error = string.Format("server couldn't handle the request")
            });

            _logger.LogError(ex, "GetNotificationList fail with an exception");
        }

        _logger.LogInformation("GetNotificationList", "end");
        
        return result;
    }
}