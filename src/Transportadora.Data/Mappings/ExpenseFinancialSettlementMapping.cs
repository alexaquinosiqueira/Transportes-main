using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    public class ExpenseFinancialSettlementMapping : IEntityTypeConfiguration<ExpenseFinancialSettlement>
    {
        public void Configure(EntityTypeBuilder<ExpenseFinancialSettlement> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(p => p.FinancialSettlement)
                .WithMany(u => u.ExpenseFinancialSettlements)
                .HasForeignKey(x => x.FinancialSettlement_Id);

            builder.Property(x => x.Amount)
                .IsRequired();

            builder.HasOne(p => p.ExpenseType)
                .WithMany(u => u.ExpenseFinancialSettlements)
                .HasForeignKey(x => x.ExpenseType_Id);


            builder.HasOne(p => p.Supplier)
                .WithMany(u => u.ExpenseFinancialSettlements)
                .HasForeignKey(x => x.Supplier_Id);

            builder.HasOne(p => p.Employee)
                .WithMany(u => u.ExpenseFinancialSettlements)
                .HasForeignKey(x => x.Employee_Id);

            builder.Property(x => x.ExpenseDate);

            builder.Property(x => x.Litros);

            builder.Property(x => x.Valor_Unitario);

            builder.Property(x => x.Valor_Total);

            builder.Property(x => x.Consumo);

            builder.Property(x => x.Arquivo);

            builder.Property(x => x.Desconto);

            builder.ToTable("ExpenseFinancialSettlement", "dbo.Financeiro");

        }
    }
}
