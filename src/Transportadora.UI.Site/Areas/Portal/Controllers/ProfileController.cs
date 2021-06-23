using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.UI.Site.ViewModels;
using X.PagedList;

namespace Transportadora.UI.Site.Areas.Portal.Controllers
{
    [Area("Portal")]
    public class ProfileController : Controller
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IProfileActionFuncionalityRepository _profileActionFuncionalityRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IMapper _mapper;
        public ProfileController(IProfileRepository profileRepository,
                                 IModuleRepository moduleRepository,
                                 IProfileActionFuncionalityRepository profileActionFuncionalityRepository,
                                IMapper mapper)
        {
            _profileRepository = profileRepository;
            _profileActionFuncionalityRepository = profileActionFuncionalityRepository;
            _moduleRepository = moduleRepository;
            _mapper = mapper;
        }


        //public IActionResult Index(int? page, string SearchString)
        //{
        //    int pagenumber = (page ?? 1) - 1;
        //    var profiles = _profileRepository.GetPaged(pagenumber, 10, out int totalCount);
        //    IPagedList<ProfileViewModel> profileViewModels = new StaticPagedList<ProfileViewModel>(_mapper.Map<IEnumerable<ProfileViewModel>>(profiles), pagenumber + 1, 10, totalCount);

        //    return View(profileViewModels);
        //}

        public async Task<IActionResult> Index()
        {
            var profiles = await _profileRepository.GetAll();

            return View(_mapper.Map<IEnumerable<ProfileViewModel>>(profiles));
        }


        public async Task<IActionResult> Details(Guid id)
        {

            var modules = await _moduleRepository.GetAll();
            ViewData["modules"] = _mapper.Map<IEnumerable<ModuleViewModel>>(modules);
            var ProfileViewModel = _mapper.Map<ProfileViewModel>(await _profileRepository.GetById(id));
            if (ProfileViewModel == null)
            {
                return NotFound();
            }

            return View(ProfileViewModel);
        }

        public async Task<IActionResult> Create()
        {
            var modules = await _moduleRepository.GetAll();
            ViewData["modules"] = _mapper.Map<IEnumerable<ModuleViewModel>>(modules.OrderBy(m => m.ModuleOrder));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProfileViewModel ProfileViewModel)
        {
            if (!ModelState.IsValid) return View(ProfileViewModel);

            await _profileRepository.Add(_mapper.Map<ProfileT>(ProfileViewModel));

            List<ProfileActionFuncionalityViewModel> profileActionFuncionalityViewModels = new List<ProfileActionFuncionalityViewModel>();
            foreach (var item in ProfileViewModel.ActionIds)
            {
                ProfileActionFuncionalityViewModel profileActionFuncionalityViewModel = new ProfileActionFuncionalityViewModel
                {
                    Profile_Id = ProfileViewModel.Id,
                    ActionFuncionality_Id = item
                };
                profileActionFuncionalityViewModels.Add(profileActionFuncionalityViewModel);
            }

            foreach (var profileActionFuncionality in profileActionFuncionalityViewModels)
            {
                await _profileActionFuncionalityRepository.Add(_mapper.Map<ProfileActionFuncionality>(profileActionFuncionality));
            }

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var modules = await _moduleRepository.GetAll();
            ViewData["modules"] = _mapper.Map<IEnumerable<ModuleViewModel>>(modules.OrderBy(m => m.ModuleOrder));
            var ProfileViewModel = _mapper.Map<ProfileViewModel>(await _profileRepository.GetById(id));
            if (ProfileViewModel == null)
            {
                return NotFound();
            }
            return View(ProfileViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProfileViewModel ProfileViewModel)
        {
            if (id != ProfileViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(ProfileViewModel);

            var profile = _mapper.Map<ProfileT>(ProfileViewModel);
            await _profileRepository.Update(profile);


            var profiles = await _profileActionFuncionalityRepository.Search(p => p.Profile_Id == id);

            foreach (var p in profiles)
            {
                await _profileActionFuncionalityRepository.Remove(p);
            }

            List<ProfileActionFuncionalityViewModel> profileActionFuncionalityViewModels = new List<ProfileActionFuncionalityViewModel>();
            foreach (var item in ProfileViewModel.ActionIds)
            {
                ProfileActionFuncionalityViewModel profileActionFuncionalityViewModel = new ProfileActionFuncionalityViewModel
                {
                    Profile_Id = ProfileViewModel.Id,
                    ActionFuncionality_Id = item
                };
                profileActionFuncionalityViewModels.Add(profileActionFuncionalityViewModel);
            }

            foreach (var profileActionFuncionality in profileActionFuncionalityViewModels)
            {
                await _profileActionFuncionalityRepository.Add(_mapper.Map<ProfileActionFuncionality>(profileActionFuncionality));
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ProfileViewModel = _mapper.Map<ProfileViewModel>(await _profileRepository.GetById(id));

            if (ProfileViewModel == null)
            {
                return NotFound();
            }
            await _profileRepository.Remove(await _profileRepository.GetById(id));

            TempData["cls"] = "success";
            TempData["message"] = "Deletado com sucesso !!";

            return RedirectToAction(nameof(Index));
        }
    }
}
