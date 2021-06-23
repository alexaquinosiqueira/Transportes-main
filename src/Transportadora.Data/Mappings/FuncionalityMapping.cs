using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    public class FuncionalityMapping : IEntityTypeConfiguration<Funcionality>
    {
        public void Configure(EntityTypeBuilder<Funcionality> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255);


            builder.HasOne(p => p.Module)
                .WithMany(u => u.Functionalities)
                .HasForeignKey(x => x.Module_Id);

            builder.HasMany(x => x.Actions)
                .WithOne(x => x.Funcionality);

            builder.ToTable("Funcionality", "dbo.Portal");
        }
    }
}
