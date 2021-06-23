using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    public class InvoiceMapping : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(250);

			builder.HasOne(p => p.Customer)
				.WithMany(u => u.Invoices)
				.HasForeignKey(x => x.Customer_Id);

			builder.Property(x => x.Amount)
				.IsRequired();
			builder.Property(x => x.Date)
				.IsRequired();
			builder.Property(x => x.Status)
				.IsRequired();
			builder.Property(x => x.DueDate);
			builder.Property(x => x.SettlementDate);

			builder.HasOne(p => p.CostCenter)
				.WithMany(u => u.Invoices)
				.HasForeignKey(x => x.CostCenter_Id);

			builder.HasOne(p => p.BankAccount)
				.WithMany(u => u.Invoices)
				.HasForeignKey(x => x.BankAccount_Id);
			builder.HasOne(p => p.DocumentType)
				.WithMany(u => u.Invoices)
				.HasForeignKey(x => x.DocumentType_Id);
			builder.Property(x => x.DocumentNumber);
			builder.HasOne(p => p.PaymentForm)
				.WithMany(u => u.Invoices)
				.HasForeignKey(x => x.PaymentForm_Id);

			builder.Property(x => x.Observation)
				.HasMaxLength(500);
			builder.Property(x => x.LastSettlementDate);
			builder.Property(x => x.PaidAmount);

			builder.HasOne(p => p.Company)
				.WithMany()
				.HasForeignKey(x => x.Company_Id);


			builder.ToTable("Invoice", "dbo.Financeiro");
        }
    }
}
