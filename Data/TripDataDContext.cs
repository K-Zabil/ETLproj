using EFCore.BulkExtensions;
using ETLproj.Data.Configurations;
using ETLproj.Models;
using Microsoft.EntityFrameworkCore;

namespace ETLproj.Data;

public class TripDataDContext : DbContext
{
    public DbSet<TripData> Trips { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "Server=localhost,1433;Database=TripsDb;User Id=sa;Password=Secret12345!;TrustServerCertificate=True";
        optionsBuilder.UseSqlServer(connectionString);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new TripConfigurations());
    }

    public async Task BulkInsertTripsAsync(HashSet<TripData> trips)
    {
        if (trips == null || trips.Count == 0) return;

        await this.BulkInsertAsync(trips, bulkConfig =>
        {
            bulkConfig.SetOutputIdentity = true; 
            bulkConfig.IncludeGraph = true;
        });
    }
}