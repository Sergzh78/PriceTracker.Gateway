using PriceTracker.Shared.Contracts;
using PriceTracker.Shared.Infrastructure.MessageBus;

namespace PriceTracker.Notification;

public class NotificationWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public NotificationWorker(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var messageBus = scope.ServiceProvider.GetRequiredService<IMessageBus>();

                    var notification = await messageBus.ConsumeAsync<NotificationEvent>(
                        QueueNames.NotificationTasks,
                        stoppingToken);

                    if (notification != null)
                    {                        
                        await NotificateAsync(notification, stoppingToken);
                        continue; 
                    }
                }
                
                await Task.Delay(1000, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                //logger
                await Task.Delay(1000, stoppingToken);
            }
        }
    }

    private async Task NotificateAsync(NotificationEvent notification, CancellationToken stoppingToken)
    {
        //TODO: send email
        await Task.CompletedTask;
    }
}