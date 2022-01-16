using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ZHD_Manager
{
    public class TrainDatabase : DbContext
    {
        public DbSet<StationInfo> Stations { get; set; }

        public DbSet<TrainInfo> Trains { get; set; }
        public DbSet<WagonInfo> Wagons { get; set; }
        public DbSet<PlaceInfo> Places { get; set; }
        public DbSet<PersonInfo> PersonList { get; set; }
        public string DbPath { get; }
        public TrainDatabase()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "TrainDB.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLazyLoadingProxies().UseSqlite($"Data Source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<StationInfo>()
                .HasMany(station => station.DepartingTrains)
                .WithOne(train => train.StationOfDeparture)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder
                .Entity<StationInfo>()
                .HasMany(station => station.ArrivingTrains)
                .WithOne(train => train.StationOfArrival)
                .OnDelete(DeleteBehavior.Cascade);
            //modelBuilder
            //    .Entity<TrainInfo>()
            //    .HasOne(train => train.StationOfDeparture)
            //    .WithMany(station => station.DepartingTrains);
            //modelBuilder
            //    .Entity<TrainInfo>()
            //    .HasOne(train => train.StationOfArrival)
            //    .WithMany(station => station.ArrivingTrains);
            modelBuilder
                .Entity<TrainInfo>()
                .HasMany(train => train.Wagons)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder
                .Entity<WagonInfo>()
                .HasMany(wagon => wagon.Places)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class PlaceInfo
    {
        public int ID { get; set; }
        public bool IsAvailable { get; set; }
    }

    public class WagonInfo
    {
        public int ID { get; set; }
        public WagonType Type { get; set; }
        public virtual ICollection<PlaceInfo> Places { get; set; } = new HashSet<PlaceInfo>();
        public enum WagonType
        {
            Open,
            Closed
        }
    }

    public class TrainInfo
    {
        public int ID { get; set; }
        public virtual StationInfo StationOfDeparture { get; set; }
        public virtual StationInfo StationOfArrival { get; set; }
        public virtual ICollection<WagonInfo> Wagons { get; set; } = new HashSet<WagonInfo>();

        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
    }

    public class StationInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<TrainInfo> ArrivingTrains { get; set; } = new HashSet<TrainInfo>();
        public virtual ICollection<TrainInfo> DepartingTrains { get; set; } = new HashSet<TrainInfo>();
    }

    public class PersonInfo
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
