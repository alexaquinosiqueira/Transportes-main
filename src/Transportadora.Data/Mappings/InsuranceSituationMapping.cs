using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    public class InsuranceSituationMapping : IEntityTypeConfiguration<InsuranceSituation>
    {
        public void Configure(EntityTypeBuilder<InsuranceSituation> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Active)
                .IsRequired();

            builder.HasOne(p => p.Company)
                .WithMany()
                .HasForeignKey(x => x.Company_Id);

            builder.ToTable("InsuranceSituation", "dbo.Cadastro");
        }
    }
}
