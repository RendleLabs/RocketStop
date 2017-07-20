using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace RocketStop.DockingService.Data
{
    public class DockingContext : DbContext
    {
        public DockingContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Bay> Bays { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<Docking> Dockings { get; set; }

    }
}
