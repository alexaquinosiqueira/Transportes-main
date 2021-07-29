using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Codigo)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Quantidade)
                .IsRequired();

            builder.Property(x => x.Qtde_minima)
                .IsRequired();

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Active)
                .IsRequired();

            builder.HasOne(p => p.Company)
                .WithMany()
                .HasForeignKey(x => x.Company_Id);

            builder.HasOne(p => p.Supplier)
                .WithMany()
                .HasForeignKey(x => x.Supplier_Id);

            builder.ToTable("Produto", "dbo.Cadastro");
        }
    }
}
