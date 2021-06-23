using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Transportadora.UI.Site.ViewModels
{
    public class InvoiceViewModel
	{
		[Key]
		public Guid Id { get; set; }
		[DisplayName("Cliente")]
		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public Guid? Customer_Id { get; set; }
		public virtual CustomerViewModel? Customer { get; set; }
		[DisplayName("Descrição")]
		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public string Description { get; set; }
		[DisplayName("Valor Original")]
		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public decimal Amount { get; set; }
		[DisplayName("Data")]

		public DateTime? Date { get; set; }
		public StatusViewModel status;
		public StatusViewModel Status
		{
			get
			{
				if (InvoicePayments != null)
                {
					if (InvoicePayments.All(ep => ep.StatusInvoicePayment == StatusViewModel.closed))
					{
						return StatusViewModel.closed;
					}
					if (InvoicePayments.Any(ep => ep.StatusInvoicePayment == StatusViewModel.closed))
					{
						return StatusViewModel.inProgress;
					}
				}


				return StatusViewModel.opened;
			}
			set => status = value;
		}
		[DisplayName("Data de Vencimento")]
		public DateTime DueDate { get; set; }
		[DisplayName("Data de Liquidação")]
		public DateTime? SettlementDate { get; set; }
		[DisplayName("Centro de Custo")]
		public Guid CostCenter_Id { get; set; }
		public CostCenterViewModel CostCenter { get; set; }
		[DisplayName("Conta")]
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
		public DateTime? LastSettlementDate { get; set; }
		[DisplayName("Valor a Receber")]
		public decimal PaidAmount { get; set; }

		public ICollection<InvoicePaymentViewModel> InvoicePayments { get; set; }
		public Guid Company_Id { get; set; }
		public CompanyViewModel Company { get; set; }

	}
}
