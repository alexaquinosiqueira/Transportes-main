using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    public class FleetMapping : IEntityTypeConfiguration<Fleet>
    {
        public void Configure(EntityTypeBuilder<Fleet> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.Active)
                .IsRequired();

            builder.HasOne(p => p.Company)
                .WithMany()
                .HasForeignKey(x => x.Company_Id);

            builder.ToTable("Fleet", "dbo.Cadastro");
        }
    }
}
