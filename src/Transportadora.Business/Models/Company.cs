using System;
using System.Collections.Generic;

namespace Transportadora.Business.Models
{
    public class Company : Entity
    {

        public bool Status { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Contato { get; set; }
        public string InscEstatudal { get; set; }
        public string InscMunicipal { get; set; }
        public Guid Address_Id { get; set; }
        public virtual Address Address { get; set; }
        public virtual IEnumerable<UserCompany> UserCompanies { get; set; }

    }
}
