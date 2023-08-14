namespace NotificationApi.Storage.Models;

public class Notification {
    public Guid ID {get; set;}
    public required string Title {get; set;}
    public required string Description {get; set;}
}