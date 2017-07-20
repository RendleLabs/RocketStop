using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RocketStop.DockingService.Data
{
    public static class MigrationSeedData
    {
        public static async Task EnsureSeedData(this DockingContext context)
        {
            if (!await context.Bays.AnyAsync())
            {
                AddBays(context.Bays, "S", 10, 10, 100, 64);
                AddBays(context.Bays, "M", 20, 20, 200, 32);
                AddBays(context.Bays, "L", 40, 40, 400, 16);
                await context.SaveChangesAsync();
            }
        }

        private static void AddBays(DbSet<Bay> bays, string section, int width, int height, int depth, int count)
        {
            for (int i = 0; i < count; i++)
            {
                bays.Add(new Bay
                {
                    Section = section,
                    Number = count + 1,
                    Width = width,
                    Height = height,
                    Depth = depth
                });
            }
        }
    }
}