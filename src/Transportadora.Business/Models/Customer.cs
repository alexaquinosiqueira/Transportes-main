using System;
using System.Collections.Generic;

namespace Transportadora.Business.Models
{
    public class Customer : Entity
    {
        public string Name { get; set; }
        public KindPerson KindPerson { get; set; }
        public string Document { get; set; }
        public DateTime DateSince { get; set; }
        public string Observation { get; set; }
        public decimal CreditLimit { get; set; }
        //- Valor a receber
        //- Valor recebido	
        public string CelPhone { get; set; }
        public Guid Address_Id { get; set; }
        public virtual Address Address { get; set; }
        

        public string StateRegistration { get; set; }
        public string MunicipalRegistration { get; set; }
        public string GeneralRecord { get; set; }

        public string DispatchingAgency { get; set; }
        public string UFRG { get; set; }
        public DateTime? IssuanceDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public Genre? Genre { get; set; }

        public MaritalStatus? MaritalStatus { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
        public Guid Company_Id { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<FinancialSettlement> FinancialSettlements { get; set; }

    }
}
