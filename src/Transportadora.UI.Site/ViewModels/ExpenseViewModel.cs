using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Transportadora.UI.Site.ViewModels
{
    public class ExpenseViewModel
    {
		[Key]
		public Guid Id { get; set; }
		[DisplayName("Fornecedor")]
		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public Guid? Supplier_Id { get; set; }
		public SupplierViewModel Supplier { get; set; }
		[DisplayName("Descrição")]
		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public string Description { get; set; }
		[DisplayName("Valor Original")]
		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		//[DisplayFormat(DataFormatString = "{0:C}")]
		public decimal Amount { get; set; }
		[DisplayName("Data")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime? Date { get; set; }
		public StatusViewModel status;
		public StatusViewModel Status {
			get 
			{
				if (ExpensePayment.All(ep => ep.StatusExpensePayment == StatusViewModel.closed))
                {
					return StatusViewModel.closed;
                }
				if (ExpensePayment.Any(ep => ep.StatusExpensePayment == StatusViewModel.closed))
				{
					return StatusViewModel.inProgress;
				}
				
				return StatusViewModel.opened;
			}
			set => status = value; 
		}
		[DisplayName("Data de Vencimento")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]

		public DateTime DueDate { get; set; }
		[DisplayName("Data de Liquidação")]	
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime? SettlementDate { get; set; }
		
		public Guid CostCenter_Id { get; set; }
		[DisplayName("Centro de Custo")]

		public CostCenterViewModel CostCenter { get; set; }
		[DisplayName("Conta")]
		
		public Guid? Vehicle_Id { get; set; }
		public VehicleViewModel Vehicle { get; set; }
		[DisplayName("Veículo")]
		
		public Guid BankAccount_Id { get; set; }
		public BankAccountViewModel BankAccount { get; set; }

		[DisplayName("Tipo de Documento")]
		public Guid DocumentType_Id { get; set; }

		public DocumentTypeViewModel DocumentType { get; set; }
		[DisplayName("Número do Documento")]

		public int? DocumentNumber { get; set; }

		[DisplayName("Forma de Pagamento")]
		public Guid PaymentForm_Id { get; set; }

		public PaymentFormViewModel PaymentForm { get; set; }
		[DisplayName("Observação")]

		public string Observation { get; set; }
		[DisplayName("Última data de liquidação")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]

		public DateTime? LastSettlementDate { get; set; }
		[DisplayName("Valor Pago")]
		//[DisplayFormat(DataFormatString = "{0:C}")]
		public decimal PaidAmount { get; set; }

		public ICollection<ExpensePaymentViewModel> ExpensePayment { get; set; }
		public Guid Company_Id { get; set; }
		public CompanyViewModel Company { get; set; }

	}
}
