using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    public class VehicleMapping : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {

            builder.HasKey(x => x.Id);

            builder.HasOne(p => p.VehicleType)
                .WithMany(u => u.Vehicles)
                .HasForeignKey(x => x.VehicleType_Id);

            builder.HasOne(p => p.Fleet)
                .WithMany(u => u.Vehicles)
                .HasForeignKey(x => x.Fleet_Id);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.VehicleLicensePlate)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Year)
                .IsRequired();

            builder.Property(x => x.Color)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.Brand)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.Model)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.chassi)
                .IsRequired();

            builder.Property(x => x.renavam)
                .IsRequired();
            builder.Property(x => x.km)
                .IsRequired();
            builder.Property(x => x.tank)
                .IsRequired();
            builder.Property(x => x.average)
                .IsRequired();
            builder.Property(x => x.litragem)
                .IsRequired();

            builder.HasOne(p => p.Employee)
                .WithMany(u => u.Vehicles)
                .HasForeignKey(x => x.Employee_Id);

            builder.HasOne(p => p.City)
                .WithMany(u => u.Vehicles)
                .HasForeignKey(x => x.City_Id);

            builder.HasOne(p => p.Company)
                .WithMany()
                .HasForeignKey(x => x.Company_Id);

            builder.ToTable("Vehicle", "dbo.Cadastro");
        }
    }
}
