using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    public class ProfileMapping : IEntityTypeConfiguration<ProfileT>
    {
        public void Configure(EntityTypeBuilder<ProfileT> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("nvarchar(255)");

            builder.HasMany(f => f.Users)
                .WithOne(p => p.Profile)
                .HasForeignKey(p => p.ProfileId);


            builder.ToTable("Profile", "dbo.Portal");
        }
    }
}
