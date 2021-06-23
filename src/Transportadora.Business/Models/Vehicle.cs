using System;
using System.Collections.Generic;

namespace Transportadora.Business.Models
{
    public class Vehicle : Entity
    {
        public virtual ICollection<Expense> Expense { get; set; }

        public Guid VehicleType_Id { get; set; }
        public virtual VehicleType VehicleType { get; set; }
        public Guid Fleet_Id { get; set; }
        public virtual Fleet Fleet { get; set; }
        public string Description { get; set; }
        public string VehicleLicensePlate { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal chassi { get; set; }
        public decimal renavam { get; set; }
        public decimal km { get; set; }
        public decimal tank { get; set; }
        public decimal average { get; set; }
        public decimal litragem { get; set; }

        public Guid Employee_Id { get; set; }
        public virtual Employee Employee { get; set; }
        public int City_Id { get; set; }
        public virtual City City { get; set; }

        public virtual ICollection<Insurance> Insurances { get; set; }
        public virtual ICollection<FinancialSettlement> FinancialSettlements { get; set; }
        public Guid Company_Id { get; set; }
        public virtual Company Company { get; set; }
    }
}
