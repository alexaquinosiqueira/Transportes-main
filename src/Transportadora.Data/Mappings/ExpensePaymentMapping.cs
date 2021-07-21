using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    public class ExpensePaymentMapping : IEntityTypeConfiguration<ExpensePayment>
	{
		public void Configure(EntityTypeBuilder<ExpensePayment> builder)
		{
			builder.HasKey(x => x.Id);

			builder.HasOne(p => p.Expense)
				.WithMany(u => u.ExpensePayment)
				.HasForeignKey(x => x.Expense_Id);

			builder.Property(x => x.AmountExpensePayment);
			builder.Property(x => x.Valor_Pago);
			builder.Property(x => x.Valor_Juros);
			builder.Property(x => x.ConcludedDate);
			builder.Property(x => x.StatusExpensePayment);
			builder.Property(x => x.DueDateExpensePayment);


			builder.ToTable("ExpensePayment", "dbo.Financeiro");
		}
	}
}
