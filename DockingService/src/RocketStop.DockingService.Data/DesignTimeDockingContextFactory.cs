using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RocketStop.DockingService.Data
{
    public class DesignTimeDockingContextFactory : IDesignTimeDbContextFactory<DockingContext>
    {
        private const string LocalPostgres = "Host=localhost;Database=docking;Username=postgres;Password=secretsquirrel";

        public DockingContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder()
                .UseNpgsql(LocalPostgres);
            return new DockingContext(builder.Options);
        }
    }
}
