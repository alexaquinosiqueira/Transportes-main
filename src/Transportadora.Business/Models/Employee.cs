using System;
using System.Collections.Generic;

namespace Transportadora.Business.Models
{
    public class Employee : Entity
    {
        public string Name { get; set; }
        public string NickName { get; set; }
        public string CelPhone { get; set; }
        public Genre Genre { get; set; }
        public DateTime BirthDate { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public Guid Responsibility_Id { get; set; }
        public virtual Responsibility Responsibility { get; set; }
        public DateTime AdmissionDate { get; set; }
        public decimal Salary { get; set; }
        public DateTime? ResignationDate { get; set; }
        public bool Active { get; set; }

        public Guid Address_Id { get; set; }
        public virtual Address Address { get; set; }

        public string Imagem { get; set; }

        public virtual ICollection<FinancialSettlement> FinancialSettlements { get; set; }

        public virtual ICollection<ExpenseFinancialSettlement> ExpenseFinancialSettlements { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public Guid Company_Id { get; set; }
        public virtual Company Company { get; set; }

        public string CNH { get; set; }
        public int TipoCNH { get; set; }
        public DateTime DataCNH { get; set; }
        public string RG { get; set; }
        public string OrgaoExpedidor { get; set; }
        public string UFRG { get; set; }
        public DateTime DataEmissao { get; set; }
        public string CPF { get; set; }
        public string NumeroINSS { get; set; }

        public string NomePai { get; set; }
        public string NomeMae { get; set; }
        public string Filho1 { get; set; }
        public string Filho2 { get; set; }
        public string Filho3 { get; set; }
        public string Filho4 { get; set; }
        public string Filho5 { get; set; }
        public DateTime? DtNascimentoFilho1 { get; set; }
        public DateTime? DtNascimentoFilho2 { get; set; }
        public DateTime? DtNascimentoFilho3 { get; set; }
        public DateTime? DtNascimentoFilho4 { get; set; }
        public DateTime? DtNascimentoFilho5 { get; set; }


    }
}
