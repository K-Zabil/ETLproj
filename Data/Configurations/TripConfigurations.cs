using ETLproj.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETLproj.Data.Configurations;

public class TripConfigurations : IEntityTypeConfiguration<TripData>
{
       public void Configure(EntityTypeBuilder<TripData> builder)
       {
              builder.ToTable("Trips");

              builder.HasKey(t => t.Id);

              builder.Property(t => t.PickupDatetime)
                     .IsRequired()
                     .HasColumnType("DATETIME");

              builder.Property(t => t.DropoffDatetime)
                     .IsRequired()
                     .HasColumnType("DATETIME");

              builder.Property(t => t.PassengerCount)
                     .IsRequired()
                     .HasColumnType("INT");

              builder.Property(t => t.TripDistance)
                     .IsRequired()
                     .HasColumnType("DECIMAL(10,2)");

              builder.Property(t => t.StoreAndFwdFlag)
                     .IsRequired()
                     .HasColumnType("VARCHAR(3)");

              builder.Property(t => t.PULocationID)
                     .IsRequired()
                     .HasColumnType("INT");

              builder.Property(t => t.DOLocationID)
                     .IsRequired()
                     .HasColumnType("INT");

              builder.Property(t => t.FareAmount)
                     .IsRequired()
                     .HasColumnType("DECIMAL(10,2)");

              builder.Property(t => t.TipAmount)
                     .IsRequired()
                     .HasColumnType("DECIMAL(10,2)");


              builder.HasIndex(t => t.PULocationID);
              builder.HasIndex(t => t.TripDistance);
              builder.HasIndex(t => t.PickupDatetime);
              builder.HasIndex(t => t.DropoffDatetime);
              builder.ToTable(b => b.HasCheckConstraint("CHK_StoreAndFwdFlag", "StoreAndFwdFlag IN ('Yes', 'No')"));
       }
}