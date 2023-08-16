
namespace NotificationApi.Controllers.Models;

public class NotificationControllerModel {
    public Guid ID {get; set;}
    public required string Title {get; set;}
    public required string Description {get; set;}
}