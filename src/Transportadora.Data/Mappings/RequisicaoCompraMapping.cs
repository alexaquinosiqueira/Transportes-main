using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    public class RequisicaoCompraMapping : IEntityTypeConfiguration<RequisicaoCompra>
    {
        public void Configure(EntityTypeBuilder<RequisicaoCompra> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Observacao)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Code);

            builder.Property(x => x.Data_Requisicao)
                .IsRequired();

            builder.Property(x => x.Solicitante)
                .IsRequired();

            builder.Property(x => x.Solicitado_Para)
                .IsRequired();

            builder.Property(x => x.Prioridade)
                .IsRequired();

            builder.Property(x => x.Tipo_Requisicao)
                .IsRequired();

            builder.Property(x => x.Observacao)
                .IsRequired();

            builder.HasOne(p => p.Company)
                .WithMany()
                .HasForeignKey(x => x.Company_Id);

            builder.HasOne(p => p.CostCenter)
                .WithMany()
                .HasForeignKey(x => x.CostCenter_Id);

            builder.HasOne(p => p.Vehicle)
                .WithMany()
                .HasForeignKey(x => x.Solicitado_Para);

            builder.ToTable("RequisicaoCompra", "dbo.Cadastro");
        }
    }
}
