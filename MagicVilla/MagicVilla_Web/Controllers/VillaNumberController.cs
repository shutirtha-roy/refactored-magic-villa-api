using Autofac;
using AutoMapper;
using MagicVilla_Web.Codes;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Data;

namespace MagicVilla_Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberWebService _villaNumberService;
        private readonly IMapper _mapper;
        private readonly IVillaService _villaService;
        private readonly ILifetimeScope _scope;

        public VillaNumberController(IVillaNumberWebService villaNumberService, IMapper mapper, IVillaService villaService, ILifetimeScope scope)
        {
            _villaNumberService = villaNumberService;
            _mapper = mapper;
            _villaService = villaService;
            _scope = scope;
        }

        public async Task<IActionResult> IndexVillaNumber()
        {
            var villaList = new List<VillaNumberModel>();
            var token = HttpContext.Session.GetString(SessionData.SessionToken);
            var response = await _villaNumberService.GetAllAsync<APIResponse>(token);

            if (response != null && response.IsSuccess)
            {
                villaList = JsonConvert.DeserializeObject<List<VillaNumberModel>>(Convert.ToString(response.Result));
            }

            return View(villaList);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateVillaNumber()
        {
            var villaNumberVM = _scope.Resolve<VillaNumberCreateVM>();
            villaNumberVM.VillaNumber = _scope.Resolve<VillaNumberCreateModel>();
            var token = HttpContext.Session.GetString(SessionData.SessionToken);
            var response = await _villaService.GetAllAsync<APIResponse>(token);

            if (response != null && response.IsSuccess)
            {
                villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaModel>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }); 
            }

            return View(villaNumberVM);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateVM model)
        {
            var token = HttpContext.Session.GetString(SessionData.SessionToken);

            if (ModelState.IsValid)
            {
                var response = await _villaNumberService.CreateAsync<APIResponse>(model.VillaNumber, token);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }

                ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
            }

            var villaResponse = await _villaService.GetAllAsync<APIResponse>(token);

            if (villaResponse != null && villaResponse.IsSuccess)
            {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaModel>>
                    (Convert.ToString(villaResponse.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }

            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateVillaNumber(int villaNo)
        {
            var villaNumberEditVM = _scope.Resolve<VillaNumberEditVM>();
            villaNumberEditVM.VillaNumber = _scope.Resolve<VillaNumberEditModel>();
            var token = HttpContext.Session.GetString(SessionData.SessionToken);
            var response = await _villaNumberService.GetAsync<APIResponse>(villaNo, token);

            if (response != null && response.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<VillaNumberModel>(Convert.ToString(response.Result));
                villaNumberEditVM.VillaNumber = _mapper.Map<VillaNumberEditModel>(model);
            }

            var responseVilla = await _villaService.GetAllAsync<APIResponse>(token);

            if (responseVilla != null && responseVilla.IsSuccess)
            {
                villaNumberEditVM.VillaList = JsonConvert.DeserializeObject<List<VillaModel>>
                    (Convert.ToString(responseVilla.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });

                return View(villaNumberEditVM);
            }

            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberEditVM model)
        {
            var token = HttpContext.Session.GetString(SessionData.SessionToken);

            if (ModelState.IsValid)
            {
                var response = await _villaNumberService.UpdateAsync<APIResponse>(model.VillaNumber, token);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }

                ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
            }

            var villaResponse = await _villaService.GetAllAsync<APIResponse>(token);

            if (villaResponse != null && villaResponse.IsSuccess)
            {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaModel>>
                    (Convert.ToString(villaResponse.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }

            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteVillaNumber(int villaNo)
        {
            var villaNumberDeleteVM = _scope.Resolve<VillaNumberDeleteVM>();
            villaNumberDeleteVM.VillaNumber = _scope.Resolve<VillaNumberDeleteModel>();
            var token = HttpContext.Session.GetString(SessionData.SessionToken);
            var response = await _villaNumberService.GetAsync<APIResponse>(villaNo, token);

            if (response != null && response.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<VillaNumberModel>(Convert.ToString(response.Result));
                villaNumberDeleteVM.VillaNumber = _mapper.Map<VillaNumberDeleteModel>(model);
            }

            var responseVilla = await _villaService.GetAllAsync<APIResponse>(token);

            if (responseVilla != null && responseVilla.IsSuccess)
            {
                villaNumberDeleteVM.VillaList = JsonConvert.DeserializeObject<List<VillaModel>>
                    (Convert.ToString(responseVilla.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });

                return View(villaNumberDeleteVM);
            }

            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVillaNumber(VillaNumberDeleteVM model)
        {
            var token = HttpContext.Session.GetString(SessionData.SessionToken);
            var response = await _villaNumberService.DeleteAsync<APIResponse>(model.VillaNumber.VillaNo, token);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexVillaNumber));
            }

            return View(model);
        }
    }
}