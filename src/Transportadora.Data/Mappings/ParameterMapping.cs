using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    class ParameterMapping : IEntityTypeConfiguration<Parameter>
    {
        public void Configure(EntityTypeBuilder<Parameter> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ParameterAtive)
                .IsRequired();

            builder.HasOne(p => p.Company)
                .WithMany()
                .HasForeignKey(x => x.Id_Company);

            builder.HasOne(p => p.DocumentType)
                .WithMany()
                .HasForeignKey(x => x.Id_DocumentType);

            builder.HasOne(p => p.BankAccount)
                .WithMany()
                .HasForeignKey(x => x.Id_BankAccount);

            builder.HasOne(p => p.PaymentForm)
                .WithMany()
                .HasForeignKey(x => x.Id_PaymentForm);

            builder.HasOne(p => p.CostCenter)
                .WithMany()
                .HasForeignKey(x => x.Id_CostCenter);

            builder.HasOne(p => p.StateOrigin)
                .WithMany()
                .HasForeignKey(x => x.StateOrigin_Id);

            builder.HasOne(p => p.CityOrigin)
                .WithMany()
                .HasForeignKey(x => x.CityOrigin_Id);

            builder.ToTable("Parameters", "dbo.Portal");
        }
    }

}
