using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("nvarchar(255)");

            builder.Property(p => p.Login)
                .IsRequired()
                .HasColumnType("nvarchar(255)");

            builder.Property(p => p.Password)
                .IsRequired()
                .HasColumnType("nvarchar(255)");

            builder.Property(p => p.ExpirationDate);

            builder.HasOne(p => p.Profile)
                .WithMany(u => u.Users)
                .HasForeignKey(x => x.ProfileId);

            builder.Property(p => p.ProfileId)
                .HasColumnName("Profile_Id");

            builder.Property(p => p.Active);

            builder.ToTable("User", "dbo.Portal");
        }
    }

}
