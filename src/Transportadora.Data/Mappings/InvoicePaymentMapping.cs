using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    public class InvoicePaymentMapping : IEntityTypeConfiguration<InvoicePayment>
	{
		public void Configure(EntityTypeBuilder<InvoicePayment> builder)
		{
			builder.HasKey(x => x.Id);

			builder.HasOne(p => p.Invoice)
				.WithMany(u => u.InvoicePayments)
				.HasForeignKey(x => x.Invoice_Id);

			builder.Property(x => x.AmountInvoicePayment);
			builder.Property(x => x.ConcludedDate);
			builder.Property(x => x.StatusInvoicePayment);
			builder.Property(x => x.DueDateInvoicePayment);


			builder.ToTable("InvoicePayment", "dbo.Financeiro");
		}
	}
}
