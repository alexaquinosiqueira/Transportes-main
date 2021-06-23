using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {

            builder.HasKey(x => x.Id);

            builder.HasOne(p => p.Address)
                .WithMany(u => u.Customers)
                .HasForeignKey(x => x.Address_Id);


            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.KindPerson)
                .IsRequired();

            builder.Property(x => x.Document)
                .IsRequired();

            builder.Property(x => x.DateSince);

            builder.Property(x => x.Observation);

            builder.Property(x => x.CreditLimit);

            builder.Property(x => x.CelPhone);

            builder.Property(x => x.StateRegistration);

            builder.Property(x => x.MunicipalRegistration);

            builder.Property(x => x.GeneralRecord);

            builder.Property(x => x.DispatchingAgency);

            builder.Property(x => x.UFRG);

            builder.Property(x => x.IssuanceDate);

            builder.Property(x => x.BirthDate);

            builder.Property(x => x.Genre);

            builder.Property(x => x.MaritalStatus);
            builder.HasOne(p => p.Company)
                .WithMany()
                .HasForeignKey(x => x.Company_Id);


            builder.ToTable("Customer", "dbo.Cadastro");
        }
    }
}
