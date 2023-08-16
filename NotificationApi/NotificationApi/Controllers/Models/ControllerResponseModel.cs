namespace NotificationApi.Controllers.Models;

public class ControllerResponseModel
{
    public bool Success 
    {  
        get 
        { 
            return true; 
        }
    }
    public object? Data { get; set; }
}