using NotificationApi.Services.Interfaces;
using NotificationApi.Repositories.Interfaces;
using NotificationApi.Storage.Interfaces;
using NotificationApi.Repositories;
using NotificationApi.Services;
using NotificationApi.Storage;
using NotificationApi.SignalRHub;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddSingleton<IStorage, InMemoryContext>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<INotifierService, NotifierService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var NOTIFICATION_UI_HOST = Environment.GetEnvironmentVariable("NOTIFICATION_UI_HOST");
if(string.IsNullOrWhiteSpace(NOTIFICATION_UI_HOST))
{
    NOTIFICATION_UI_HOST = builder.Configuration.GetValue<string>("NotificationUIHost");
    if (string.IsNullOrWhiteSpace(NOTIFICATION_UI_HOST)) 
    {
        throw new Exception("NotificationUIHost is undefined");
    }
}

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
            {
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials()
                       .WithOrigins(NOTIFICATION_UI_HOST);
            }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

// app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "api/v{version:int}/{controller=Notification}/{action=GetNotificationList}/{id?}"
);

app.UseCors("CorsPolicy");
app.MapHub<NotificationHub>("api/v1/public/boradcast");

app.Run(Environment.GetEnvironmentVariable("ASPNETCORE_SERVER_HOST"));
