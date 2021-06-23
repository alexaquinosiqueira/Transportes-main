using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    public class StateMapping : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.Uf)
                .IsRequired()
                .HasMaxLength(2);
        }
    }
}
