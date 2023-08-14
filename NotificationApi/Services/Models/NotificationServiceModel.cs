namespace NotificationApi.Services.Models;

public class NotificationServiceModel {
    public Guid ID {get; set;}
    public required string Title {get; set;}
    public required string Description {get; set;}

}