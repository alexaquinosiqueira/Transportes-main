using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    public class ProfileActionFuncionalityMapping : IEntityTypeConfiguration<ProfileActionFuncionality>
    {
        public void Configure(EntityTypeBuilder<ProfileActionFuncionality> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Profile)
                .WithMany(p => p.ProfileActionFuncionalities)
                .HasForeignKey(p => p.Profile_Id);

            builder.HasOne(p => p.ActionFuncionality)
                .WithMany(p => p.ProfileActionFuncionalities)
                .HasForeignKey(p => p.ActionFuncionality_Id);

            builder.ToTable("ProfileActionFuncionality", "dbo.Portal");
        }
    }
}
