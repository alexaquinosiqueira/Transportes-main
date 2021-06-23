using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.Reporting.WebForms;

namespace Transportadora.UI.Site.Areas.Financeiro.Controllers
{
    public class RelatorioLiquidoMotoristaController : Controller
    {
        public IActionResult Index()
        {
            //var viewer = new ReportViewer();

            //viewer.ProcessingMode = ProcessingMode.Local;
            //viewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"CaminhoDoSeuRelatorio.rdlc";
            //viewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("NomeDoDataSetNoRelatorio", (System.Data.DataTable)ds.NomeDaDataTable));

            //ViewBag.ReportViewer = viewer;

            return View();
        }
    }
}
