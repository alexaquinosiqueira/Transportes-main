using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    public class PaymentFormMapping : IEntityTypeConfiguration<PaymentForm>
    {
        public void Configure(EntityTypeBuilder<PaymentForm> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.Interval)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasOne(p => p.Company)
                .WithMany()
                .HasForeignKey(x => x.Company_Id);


            builder.ToTable("PaymentForm", "dbo.Cadastro");
        }
    }
}
