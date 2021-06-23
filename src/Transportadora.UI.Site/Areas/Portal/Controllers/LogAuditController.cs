using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Transportadora.Business.Interfaces;
using Transportadora.UI.Site.ViewModels;

namespace Transportadora.UI.Site.Areas.Portal.Controllers
{
    [Area("Portal")]
    public class LogAuditController : Controller
    {
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly IMapper _mapper;

        public LogAuditController(IAuditLogRepository auditLogRepository,
                                   IMapper mapper)
        {
            _auditLogRepository = auditLogRepository;
            _mapper = mapper;
        }

        // GET: Portal/Companies
        public async Task<IActionResult> Index()
        {
            var logs = await _auditLogRepository.GetAll();

            return View(_mapper.Map<IEnumerable<AuditLogViewModel>>(logs));
        }

        // GET: Portal/Companies/Details/5
        public async Task<IActionResult> Details(long id)
        {

            var logViewModel = _mapper.Map<AuditLogViewModel>(await _auditLogRepository.GetById(id));
            if (logViewModel == null)
            {
                return NotFound();
            }

            return View(logViewModel);

        }

    }
}
