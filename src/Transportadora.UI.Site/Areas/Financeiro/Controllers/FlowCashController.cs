using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transportadora.Business.Interfaces;
using Transportadora.UI.Site.ViewModels;

namespace Transportadora.UI.Site.Areas.Financeiro.Controllers
{
    [Area("Financeiro")]
    public class FlowCashController : Controller
    {
        private readonly IFluxoCaixaRepository _fluxoCaixaRepository;
        private readonly IMapper _mapper;

        public FlowCashController(IFluxoCaixaRepository fluxoCaixaRepository,
            IMapper mapper)
        {
            _fluxoCaixaRepository = fluxoCaixaRepository;
            _mapper = mapper;
        }
        public IActionResult Index(int ano)
        {
            if (ano == 0)
                ano = DateTime.Now.Year;

            var fluxosCaixa = _mapper.Map<IEnumerable<FluxoCaixaViewModel>>( _fluxoCaixaRepository.GetFluxoCaixa(ano).ToList());

            return View(fluxosCaixa);
        }
    }
}
