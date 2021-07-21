using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    public class ExpenseSupplierMapping : IEntityTypeConfiguration<ExpenseSupplier>
    {
        public void Configure(EntityTypeBuilder<ExpenseSupplier> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(p => p.Supplier)
                .WithMany(u => u.ExpenseSupplier)
                .HasForeignKey(x => x.Supplier_Id);

            builder.HasOne(p => p.ExpenseType)
                .WithMany(u => u.ExpenseSupplier)
                .HasForeignKey(x => x.ExpenseType_Id);

            builder.ToTable("ExpenseSupplier", "dbo.Financeiro");
        }
    }
}
