using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    public class EmployeeMapping : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.NickName)
                .HasMaxLength(100);

            builder.Property(x => x.CelPhone)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Genre)
                .IsRequired();

            builder.Property(x => x.BirthDate)
                .IsRequired();


            builder.Property(x => x.MaritalStatus)
                .IsRequired();


            builder.Property(x => x.AdmissionDate)
                .IsRequired();


            builder.Property(x => x.ResignationDate);


            builder.Property(x => x.Active)
                .IsRequired();


            builder.Property(x => x.Salary)
                .IsRequired();

            builder.HasOne(p => p.Responsibility)
                .WithMany(u => u.Employees)
                .HasForeignKey(x => x.Responsibility_Id);

            builder.HasOne(p => p.Address)
                .WithMany(u => u.Employees)
                .HasForeignKey(x => x.Address_Id);

            builder.Property(p => p.Imagem)
                .HasMaxLength(250);


            builder.HasOne(p => p.Company)
                .WithMany()
                .HasForeignKey(x => x.Company_Id);

            builder.Property(x => x.CNH)
                .IsRequired();

            builder.Property(x => x.TipoCNH)
    .IsRequired();

            builder.Property(x => x.DataCNH)
    .IsRequired();
            builder.Property(x => x.RG)
.IsRequired();
            builder.Property(x => x.OrgaoExpedidor)
.IsRequired();
            builder.Property(x => x.UFRG)
.IsRequired();
            builder.Property(x => x.DataEmissao)
.IsRequired();
            builder.Property(x => x.CPF)
.IsRequired();
            builder.Property(x => x.NumeroINSS)
.IsRequired();

            builder.Property(p => p.NomeMae)
                .HasMaxLength(250);

            builder.Property(p => p.NomePai)
    .HasMaxLength(250);


            builder.Property(p => p.Filho1)
    .HasMaxLength(250);

            builder.Property(p => p.Filho2)
    .HasMaxLength(250);

            builder.Property(p => p.Filho3)
    .HasMaxLength(250);

            builder.Property(p => p.Filho4)
    .HasMaxLength(250);

            builder.Property(p => p.Filho5)
    .HasMaxLength(250);

            builder.Property(p => p.DtNascimentoFilho1);
            builder.Property(p => p.DtNascimentoFilho2);
            builder.Property(p => p.DtNascimentoFilho3);
            builder.Property(p => p.DtNascimentoFilho4);
            builder.Property(p => p.DtNascimentoFilho5);

            builder.ToTable("Employee", "dbo.Cadastro");
        }
    }
}
