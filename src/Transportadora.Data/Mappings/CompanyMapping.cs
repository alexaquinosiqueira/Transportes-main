using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    public class CompanyMapping : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(p => p.Address)
                .WithMany(u => u.Companies)
                .HasForeignKey(x => x.Address_Id);

            builder.Property(x => x.CNPJ)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.RazaoSocial)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.InscEstatudal)
                .HasMaxLength(255);


            builder.Property(x => x.InscMunicipal)
                .HasMaxLength(255);


            builder.Property(x => x.Email)
                .HasMaxLength(255);


            builder.Property(x => x.Telefone)
                .HasMaxLength(255);

            builder.Property(x => x.Status)
                .IsRequired();

            builder.ToTable("Company", "dbo.Portal");
        }


    }
}
