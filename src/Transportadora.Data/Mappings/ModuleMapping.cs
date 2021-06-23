using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    public class ModuleMapping : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {


            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255);


            builder.HasMany(x => x.Functionalities)
                .WithOne(x => x.Module);

            builder.ToTable("Module", "dbo.Portal");
        }
    }
}
