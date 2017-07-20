using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using Polly;

namespace RocketStop.DockingService.Data
{
    public class MigrationHelper
    {
        private readonly ILogger<MigrationHelper> _logger;

        public MigrationHelper(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MigrationHelper>();
        }

        public async Task TryMigrate(DbContextOptions<DockingContext> options)
        {
            await TryMigrate(new DockingContext(options));
        }

        public async Task TryMigrate(DockingContext context)
        {
            using (context)
            {
                await TryConnect(context);

                await TryRunMigration(context);

                await context.EnsureSeedData();
            }
        }

        private static async Task TryRunMigration(DockingContext context)
        {
            try
            {
                await context.Database.MigrateAsync();
            }
            catch
            {
                // Ignored
            }
        }

        private async Task TryConnect(DockingContext context)
        {
            try
            {
            await Policy
                .Handle<NpgsqlException>((ex) =>
                {
                    _logger.LogWarning(EventIds.MigrationTestConnectFailed, ex, "TryMigrate test connect failed, retrying.");
                    return true;
                })
                .WaitAndRetryAsync(5, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)))
                .ExecuteAsync(async () =>
                {
                    await context.Database.OpenConnectionAsync();
                    context.Database.CloseConnection();
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(EventIds.MigrationTestConnectFailed, ex, "TryMigrate could not connect to database.");
                throw;
            }
        }
    }
}
