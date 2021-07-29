using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transportadora.Business.Models;

namespace Transportadora.Data.Mappings
{
    public class FinancialSettlementMapping : IEntityTypeConfiguration<FinancialSettlement>
    {
        public void Configure(EntityTypeBuilder<FinancialSettlement> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Date)
                .IsRequired();

            builder.Property(x => x.TravelDate)
                .IsRequired(); 


            builder.HasOne(p => p.Customer)
                .WithMany(u => u.FinancialSettlements)
                .HasForeignKey(x => x.Customer_Id);

            builder.HasOne(p => p.Fleet)
                .WithMany(u => u.FinancialSettlements)
                .HasForeignKey(x => x.Fleet_Id);

            builder.HasOne(p => p.Vehicle)
                .WithMany(u => u.FinancialSettlements)
                .HasForeignKey(x => x.Vehicle_Id);

            builder.HasOne(p => p.Employee)
                .WithMany(u => u.FinancialSettlements)
                .HasForeignKey(x => x.Employee_Id);


            builder.HasOne(p => p.CityOrigin)
                .WithMany(u => u.FinancialSettlementsCityOrigin)
                .HasForeignKey(x => x.CityOrigin_Id);

            builder.HasOne(p => p.DestinationCity)
                .WithMany(u => u.FinancialSettlementsDestinationCity)
                .HasForeignKey(x => x.DestinationCity_Id);

            builder.Property(x => x.TotalShipping)
                .IsRequired();

            builder.Property(x => x.Advance)
                .IsRequired();

            builder.Property(x => x.TravelPercentage)
                .IsRequired();

            builder.Property(x => x.TravelPercAdiantamento);

            builder.Property(x => x.TravelNumber);

            builder.Property(x => x.Consumo);

            builder.Property(x => x.Litros);

            builder.Property(x => x.Imagem);

            builder.Property(x => x.KmInitial)
                .IsRequired();

            builder.Property(x => x.FinalKm)
                .IsRequired();

            builder.HasOne(p => p.Company)
                .WithMany()
                .HasForeignKey(x => x.Company_Id);

            builder.ToTable("FinancialSettlement", "dbo.Financeiro");
        }
    }
}
