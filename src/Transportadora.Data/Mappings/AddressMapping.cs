using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.AddressLine)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasOne(p => p.City)
                .WithMany(u => u.Addresses)
                .HasForeignKey(x => x.City_Id);

            builder.HasOne(p => p.State)
                .WithMany(u => u.Addresses)
                .HasForeignKey(x => x.State_id);

            builder.Property(x => x.Number)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Neighborhood)
                .IsRequired()
                .HasMaxLength(255);


            builder.Property(x => x.PostalCode)
                .IsRequired()
                .HasMaxLength(25);

            builder.ToTable("Address", "dbo.Cadastro");
        }
    }
}
