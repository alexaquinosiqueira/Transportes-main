using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    public class CityMapping : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.Uf)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasOne(p => p.State)
                .WithMany(u => u.Cities)
                .HasForeignKey(x => x.Uf);
        }
    }
}
