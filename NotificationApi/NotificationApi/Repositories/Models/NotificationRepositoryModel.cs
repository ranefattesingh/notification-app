namespace NotificationApi.Repositories.Models;

public class NotificationRepositoryModel {
    public Guid ID {get; set;}
    public required string Title {get; set;}
    public required string Description {get; set;}
}