using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Transportadora.UI.Site.ViewModels
{
    public class FinancialSettlementViewModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        [DisplayName("Data da Viagem")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]

        public DateTime TravelDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Data Viagem")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]

        public string Code { get; set; }
        [DisplayName("Descrição")]

        public string Description { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid? Customer_Id { get; set; }
        [DisplayName("Cliente")]
        public CustomerViewModel Customer { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]

        public Guid Fleet_Id { get; set; }
        [DisplayName("Frota")]

        public FleetViewModel Fleet { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Veículo")]

        public Guid Vehicle_Id { get; set; }

        public VehicleViewModel Vehicle { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Motorista")]

        public Guid Employee_Id { get; set; }

        public EmployeeViewModel Employee { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Cidade Origem")]

        public int? CityOrigin_Id { get; set; }

        public CityViewModel CityOrigin { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Cidade Destino")]


        public int DestinationCity_Id { get; set; }

        public CityViewModel DestinationCity { get; set; }
        [DisplayName("Valor Frete")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.Currency)]

        public decimal TotalShipping { get; set; }
        [DisplayName("Adiantamento")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.Currency)]

        public decimal Advance { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Percentual Comissão")]

        public decimal TravelPercentage { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Percentual")]

        public decimal TravelPercAdiantamento { get; set; }
        [DisplayName("Percentual de Adiantamento")]

        public string TravelNumber { get; set; }
        [DisplayName("Número da Viagem")]

        public int KmInitial { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Km Final")]

        public int FinalKm { get; set; }

        public IFormFile ImagemUpload { get; set; }

        public string Imagem { get; set; }

        public decimal vresultado { get; set; }


        public decimal Litros
        {
            get
            {
                return (ExpenseFinancialSettlements == null ? 0 : Math.Round(ExpenseFinancialSettlements.Sum(x => x.Litros), 2));
            }
        }

        public int KmTotal
        {
            get
            {
                return FinalKm - KmInitial;
            }
        }
        [DataType(DataType.Currency)]

        public decimal Consumo
        {
            get
            {
                if (Litros != 0)
                    return (ExpenseFinancialSettlements == null ? 0 : Math.Round(KmTotal / Litros, 2));
                else
                    return 0;
            }

        }

        public decimal ExpenseTotal
        {
            get
            {
                vresultado = 0;

                if (ExpenseFinancialSettlements != null)
                {
                    foreach (var item in ExpenseFinancialSettlements)
                    {
                        if (item.Arquivo != "Performance Motorista")
                        {
                            vresultado += Math.Round(item.Valor_Total - item.Desconto, 2);
                        }
                    }
                }
                return vresultado; //(ExpenseFinancialSettlements == null ? 0 : Math.Round(ExpenseFinancialSettlements.Sum(x => x.Valor_Total), 2)); 
            }
        }


        [DataType(DataType.Currency)]

        public decimal Commission
        {
            get
            {
                return Math.Round(TotalShipping * (TravelPercentage / 100), 2);
            }
        }


        [DataType(DataType.Currency)]
        public decimal Incoming
        {
            get
            {
                return Math.Round(Advance - (ExpenseTotal + Commission), 2);
            }
        }

        public bool IsInvoice()
        {
            if (Incoming > 0)
                return true;
            return false;

        }

        public ICollection<ExpenseFinancialSettlementViewModel> ExpenseFinancialSettlements { get; set; }
        public Guid Company_Id { get; set; }
        public CompanyViewModel Company { get; set; }
    }
}
