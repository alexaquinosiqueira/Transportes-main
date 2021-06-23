using System;

namespace Transportadora.Business.Models
{
    public class Parameter : Entity
    {
        public Guid Id_CostCenter { get; set; }
        public virtual CostCenter CostCenter { get; set; }
        public Guid Id_BankAccount { get; set; }
        public virtual BankAccount BankAccount { get; set; }
        public Guid Id_DocumentType { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public Guid Id_PaymentForm { get; set; }
        public virtual PaymentForm PaymentForm { get; set; }
        public Guid Id_Company { get; set; }
        public virtual Company Company { get; set; }
        public bool ParameterAtive { get; set; }
        public int StateOrigin_Id { get; set; }
        public virtual State StateOrigin { get; set; }
        public int CityOrigin_Id { get; set; }
        public virtual City CityOrigin { get; set; }

    }
}
