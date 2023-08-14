namespace NotificationApi.Controllers.Models;

public class ControllerErrorResponseModel
{
    public bool Success 
    { 
        get 
        {
            return false;
        } 
    }
    public required string Error { get; set; }
}