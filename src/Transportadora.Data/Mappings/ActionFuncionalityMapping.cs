using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using Transportadora.Business.Models;


namespace Transportadora.Data.Mappings
{
    public class ActionFuncionalityMapping : IEntityTypeConfiguration<ActionFuncionality>
    {
        public void Configure(EntityTypeBuilder<ActionFuncionality> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Role)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Name)
                .HasMaxLength(255);

            builder.HasOne(x => x.Funcionality).
                WithMany(x => x.Actions)
                .HasForeignKey(x => x.Funcionality_Id) ;

            builder.ToTable("ActionFuncionality", "dbo.Portal");
        }
    }
}
