using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Transportadora.UI.Site.ViewModels
{
    public class SupplierViewModel
    {
		[Key]
		public Guid? Id { get; set; }

		[DisplayName("Nome")]
		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public string Name { get; set; }
		[DisplayName("Física ou Jurídica")]
		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public KindPersonViewModel KindPerson { get; set; }
		[DisplayName("Documento")]
		public string Document { get; set; }
		[DisplayName("Contato Desde")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime DateSince { get; set; }
		[DisplayName("Observação")]
		public string Observation { get; set; }
		[DisplayName("Limite de Crédito")]
		[DisplayFormat(DataFormatString = "{0:C}")]
		public decimal CreditLimit { get; set; }
		//- Valor a Pagar
		//- Valor Pago
		[DisplayName("Telefone")]
		public string CelPhone { get; set; }
		[DisplayName("Endereço")]
		public Guid Address_Id { get; set; }
		public AddressViewModel Address { get; set; }
		[DisplayName("Incrição Estadual")]
		public string StateRegistration { get; set; }
		[DisplayName("Incrição Municipal")]
		public string MunicipalRegistration { get; set; }
		[DisplayName("RG")]
		public string GeneralRecord { get; set; }
		[DisplayName("Orgão Expeditor")]
		public string DispatchingAgency { get; set; }
		[DisplayName("UFRG")]
		public string UFRG { get; set; }
		[DisplayName("Data Emissão")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime? IssuanceDate { get; set; }
		[DisplayName("Data de Nascimento")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime? BirthDate { get; set; }
		[DisplayName("Sexo")]
		public GenreViewModel? Genre { get; set; }
		[DisplayName("Estado Civil")]
		public MaritalStatusViewModel? MaritalStatus { get; set; }
		public ICollection<ExpenseViewModel> Expenses { get; set; }
		public Guid Company_Id { get; set; }
		public CompanyViewModel Company { get; set; }
		public Guid? ExpenseType_Id { get; set; }
		public ExpenseTypeViewModel ExpenseType { get; set; }
	}
}
