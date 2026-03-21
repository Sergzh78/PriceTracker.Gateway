using Microsoft.EntityFrameworkCore;
using PriceTracker.Shared.Constants;
using PriceTracker.Shared.Contracts;
using PriceTracker.Shared.Infrastructure.MessageBus;
using PriceTracker.Wildberries.Worker.Data;
using PriceTracker.Wildberries.Worker.Entities;
using PriceTracker.Wildberries.Worker.Services;

namespace PriceTracker.Wildberries.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public Worker(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var tasks = await GetTasksAsync(stoppingToken);

                    if (tasks.Count == 0)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                        continue;
                    }

                    await ProcessTasksAsync(tasks, stoppingToken);

                    await Task.Delay(100, stoppingToken);
                }
                catch (OperationCanceledException oe)
                {
                    //logger
                    break;
                }
                catch (Exception ex)
                {
                    //logger
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
            }
        }

        private async Task<List<WbTask>> GetTasksAsync(CancellationToken stoppingToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<WbDbContext>();

            return await dbContext.WbTasks
                .Where(x => x.LastParseDate < DateTime.UtcNow.AddMinutes(GlobalSettings.Wb.GetPriceIntervalMinutes))
                .Where(x => x.ParseToDate <= DateTime.UtcNow)
                .Where(x => x.Status == TStatus.Active)
                .OrderBy(x => x.LastParseDate)
                .Take(5)
                .ToListAsync(stoppingToken);
        }

        private async Task ProcessTasksAsync(List<WbTask> tasks, CancellationToken stoppingToken)
        {
            var parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = 5,
                CancellationToken = stoppingToken
            };

            await Parallel.ForEachAsync(tasks, parallelOptions, async (task, token) =>
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<WbDbContext>();
                var messageBus = scope.ServiceProvider.GetRequiredService<IMessageBus>();
                var wbParserApi = scope.ServiceProvider.GetRequiredService<IWbParserApi>();

                try
                {
                    var freshTask = await dbContext.WbTasks
                        .FirstOrDefaultAsync(t => t.Id == task.Id, token);

                    if (freshTask == null)
                    {
                        //logger
                        return;
                    }

                    var responseData = await wbParserApi.GetPriceAsync(freshTask.Url, token);

                    if (responseData != null)
                    {
                        freshTask.LastParseDate = DateTime.UtcNow;

                        var history = new PriceHistory
                        {
                            Id = Guid.NewGuid(),
                            WbTaskId = freshTask.Id,
                            Price = responseData.Ñost,
                            CheckedAt = DateTime.UtcNow
                        };

                        await dbContext.PriceHistories.AddAsync(history, token);

                        if (responseData.Ñost <= freshTask.ThresholdPrice)
                        {
                            await messageBus.PublishAsync(
                                new NotificationEvent
                                {
                                    ProductName = freshTask.Name,
                                    Price = responseData.Ñost,
                                    Email = freshTask.Email
                                },
                                QueueNames.NotificationTasks,
                                token
                            );
                        }

                        await dbContext.SaveChangesAsync(token);
                    }
                }
                catch (Exception ex)
                {
                    //logger
                }
            });
        }
    }
}

