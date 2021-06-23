using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    public class BankAccountMapping : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.Branch)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Account)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Overdraft);

            builder.Property(x => x.OpeningBalance)
                .IsRequired();

            builder.Property(x => x.CurrentBalance)
                .IsRequired();

            builder.HasOne(p => p.Bank)
                .WithMany(u => u.BankAccounts)
                .HasForeignKey(x => x.Bank_Id);

            builder.HasOne(p => p.Company)
                .WithMany()
                .HasForeignKey(x => x.Company_Id);

            builder.ToTable("BankAccount", "dbo.Financeiro");
        }
    }
}
