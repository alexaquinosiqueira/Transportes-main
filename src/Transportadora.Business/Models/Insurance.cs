using System;

namespace Transportadora.Business.Models
{
    public class Insurance : Entity
    {
        public Guid Vehicle_Id { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public Guid InsuranceCompany_Id { get; set; }
        public virtual InsuranceCompany InsuranceCompany { get; set; }
        public string Apolice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Colision { get; set; }
        public decimal Stole { get; set; }
        public decimal Explosion { get; set; }
        public decimal MaterialTheft { get; set; }
        public decimal ThirdDamage { get; set; }
        public decimal Thunderbolt { get; set; }
        public decimal Value { get; set; }
        public Guid InsuranceSituation_Id { get; set; }
        public virtual InsuranceSituation InsuranceSituation { get; set; }
        public Guid Company_Id { get; set; }
        public virtual Company Company { get; set; }
    }
}
