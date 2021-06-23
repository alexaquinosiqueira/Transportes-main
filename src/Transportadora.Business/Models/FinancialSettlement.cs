using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Transportadora.Business.Models
{
    public class FinancialSettlement : Entity
    {
        public DateTime Date { get; set; }
        public DateTime TravelDate { get; set; }
        public string Code { get; set; }

        public string Description { get; set; }
        public Guid? Customer_Id { get; set; }
        public virtual Customer Customer { get; set; }
        public Guid? Fleet_Id { get; set; } 
        public virtual Fleet Fleet { get; set; }
        public Guid Vehicle_Id { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public Guid Employee_Id { get; set; }
        public virtual Employee Employee { get; set; }
        public int? CityOrigin_Id { get; set; }
        public virtual City CityOrigin { get; set; }
        public int DestinationCity_Id { get; set; }
        public virtual City DestinationCity { get; set; }
        public decimal TotalShipping { get; set; }
        public decimal Advance { get; set; }
        public decimal TravelPercentage { get; set; }

        public decimal TravelPercAdiantamento { get; set; }
        public decimal KmInitial { get; set; }
        public decimal FinalKm { get; set; }
        public string TravelNumber { get; set; } 

        public decimal Litros { get; set; }

        public decimal Consumo { get; set; }
        public string Imagem { get; set; }

        public virtual ICollection<ExpenseFinancialSettlement> ExpenseFinancialSettlements { get; set; }
        public Guid Company_Id { get; set; }
        public virtual Company Company { get; set; }
    }
}
