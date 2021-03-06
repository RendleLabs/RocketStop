﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using RocketStop.DockingService.Data;
using System;

namespace RocketStop.DockingService.Data.Migrations
{
    [DbContext(typeof(DockingContext))]
    partial class DockingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.0-preview2-25794");

            modelBuilder.Entity("RocketStop.DockingService.Data.Bay", b =>
                {
                    b.Property<int>("BayId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Depth");

                    b.Property<int>("Height");

                    b.Property<int>("Number");

                    b.Property<string>("Section");

                    b.Property<int>("Width");

                    b.HasKey("BayId");

                    b.ToTable("Bays");
                });

            modelBuilder.Entity("RocketStop.DockingService.Data.Docking", b =>
                {
                    b.Property<int>("DockingId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BayId");

                    b.Property<DateTimeOffset>("DockedAt");

                    b.Property<int>("ExpectedHours");

                    b.Property<int>("ShipId");

                    b.HasKey("DockingId");

                    b.HasIndex("BayId")
                        .IsUnique();

                    b.HasIndex("ShipId")
                        .IsUnique();

                    b.ToTable("Dockings");
                });

            modelBuilder.Entity("RocketStop.DockingService.Data.Ship", b =>
                {
                    b.Property<int>("ShipId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Depth");

                    b.Property<int>("Height");

                    b.Property<string>("Registration");

                    b.Property<int>("Width");

                    b.HasKey("ShipId");

                    b.ToTable("Ships");
                });

            modelBuilder.Entity("RocketStop.DockingService.Data.Docking", b =>
                {
                    b.HasOne("RocketStop.DockingService.Data.Bay", "Bay")
                        .WithOne("Docking")
                        .HasForeignKey("RocketStop.DockingService.Data.Docking", "BayId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RocketStop.DockingService.Data.Ship", "Ship")
                        .WithOne("Docking")
                        .HasForeignKey("RocketStop.DockingService.Data.Docking", "ShipId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
