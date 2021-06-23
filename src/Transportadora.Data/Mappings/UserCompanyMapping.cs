using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    public class UserCompanyMapping : IEntityTypeConfiguration<UserCompany>
    {
        public void Configure(EntityTypeBuilder<UserCompany> builder)
        {
            builder.HasKey(uc => uc.Id);

            builder.HasOne(uc => uc.Company)
                .WithMany(c => c.UserCompanies)
                .HasForeignKey(uc => uc.Id_Company);

            builder.HasOne(uc => uc.User)
                .WithMany(u => u.UserCompanies)
                .HasForeignKey(uc => uc.Id_User);

            builder.ToTable("UserCompany", "dbo.Portal");
        }
    }
}
