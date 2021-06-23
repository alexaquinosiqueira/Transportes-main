using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    public class InsuranceMapping : IEntityTypeConfiguration<Insurance>
    {
        public void Configure(EntityTypeBuilder<Insurance> builder)
        {

            builder.HasKey(x => x.Id);

            builder.HasOne(p => p.Vehicle)
                .WithMany(u => u.Insurances)
                .HasForeignKey(x => x.Vehicle_Id);

            builder.HasOne(p => p.InsuranceCompany)
                .WithMany(u => u.Insurances)
                .HasForeignKey(x => x.InsuranceCompany_Id);

            builder.Property(x => x.Apolice)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.StartDate)
                .IsRequired();

            builder.Property(x => x.EndDate)
                .IsRequired();


            builder.Property(x => x.Colision)
                .IsRequired();
            builder.Property(x => x.Stole)
                .IsRequired();
            builder.Property(x => x.Explosion)
                .IsRequired();
            builder.Property(x => x.MaterialTheft)
                .IsRequired();
            builder.Property(x => x.ThirdDamage)
                .IsRequired();
            builder.Property(x => x.Thunderbolt)
                .IsRequired();
            builder.Property(x => x.Value)
                .IsRequired();


            builder.HasOne(p => p.InsuranceSituation)
                .WithMany(u => u.Insurances)
                .HasForeignKey(x => x.InsuranceSituation_Id);

            builder.HasOne(p => p.Company)
                .WithMany()
                .HasForeignKey(x => x.Company_Id);


            builder.ToTable("Insurance", "dbo.Cadastro");
        }
    }
}
