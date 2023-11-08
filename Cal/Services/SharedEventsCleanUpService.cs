using Cal.Data;

namespace Cal.Services
{
    public class SharedEventsCleanupService : BackgroundService
    {

        private readonly TimeSpan _period = TimeSpan.FromMinutes(60);
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public bool IsEnabled { get; set; } = true;

        public SharedEventsCleanupService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new PeriodicTimer(_period);
            while (
                !stoppingToken.IsCancellationRequested &&
                await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    if (IsEnabled)
                    {
                        using (var scope = _serviceScopeFactory.CreateScope())
                        {
                            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                            var thresholdTime = DateTime.Now.AddHours(-24);
                            var sharedEventsToRemove = dbContext.SharedEvents.Where(se => se.CreationTime <= thresholdTime).ToList();

                            dbContext.SharedEvents.RemoveRange(sharedEventsToRemove);
                            dbContext.SaveChanges();
                        }
                    }
                    else
                    {
                        // Если не активировано
                    }
                }
                catch (Exception ex)
                {
                    // Обработка ошибки
                }
            }
        }

    }
}
