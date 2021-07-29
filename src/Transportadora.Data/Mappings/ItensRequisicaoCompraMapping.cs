using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    public class ItensRequisicaoCompraMapping : IEntityTypeConfiguration<ItensRequisicaoCompra>
    {
        public void Configure(EntityTypeBuilder<ItensRequisicaoCompra> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(p => p.RequisicaoCompra)
                .WithMany(u=> u.ItensRequisicaoCompras)
                .HasForeignKey(x => x.RequisicaoCompra_Id);

            builder.Property(x => x.Quantidade);
            builder.Property(x => x.Valor_Unitario);
            builder.Property(x => x.Valor_Total);
            builder.Property(x => x.Data_Requisicao);


            builder.HasOne(p => p.Company)
                .WithMany()
                .HasForeignKey(x => x.Company_Id);

            builder.HasOne(p => p.Supplier)
                .WithMany()
                .HasForeignKey(x => x.Supplier_Id);

            builder.HasOne(p => p.Produto)
                .WithMany()
                .HasForeignKey(x => x.Produto_Id);

            builder.ToTable("ItensRequisicaoCompra", "dbo.Cadastro");
        }
    }
}
