﻿using Autofac;
using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

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
            var response = await _villaNumberService.GetAllAsync<APIResponse>();

            if (response != null && response.IsSuccess)
            {
                villaList = JsonConvert.DeserializeObject<List<VillaNumberModel>>(Convert.ToString(response.Result));
            }

            return View(villaList);
        }

        public async Task<IActionResult> CreateVillaNumber()
        {
            var villaNumberVM = _scope.Resolve<VillaNumberCreateVM>();
            villaNumberVM.VillaNumber = _scope.Resolve<VillaNumberCreateModel>();

            var response = await _villaService.GetAllAsync<APIResponse>();

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaNumberService.CreateAsync<APIResponse>(model.VillaNumber);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }

                ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
            }

            var villaResponse = await _villaService.GetAllAsync<APIResponse>();

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

        public async Task<IActionResult> UpdateVillaNumber(int villaNo)
        {
            var villaNumberEditVM = _scope.Resolve<VillaNumberEditVM>();
            villaNumberEditVM.VillaNumber = _scope.Resolve<VillaNumberEditModel>();
            var response = await _villaNumberService.GetAsync<APIResponse>(villaNo);

            if (response != null && response.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<VillaNumberModel>(Convert.ToString(response.Result));
                villaNumberEditVM.VillaNumber = _mapper.Map<VillaNumberEditModel>(model);
            }

            var responseVilla = await _villaService.GetAllAsync<APIResponse>();

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberEditVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaNumberService.UpdateAsync<APIResponse>(model.VillaNumber);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }

                ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
            }

            var villaResponse = await _villaService.GetAllAsync<APIResponse>();

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

        public async Task<IActionResult> DeleteVillaNumber(int villaNo)
        {
            var villaNumberDeleteVM = _scope.Resolve<VillaNumberDeleteVM>();
            villaNumberDeleteVM.VillaNumber = _scope.Resolve<VillaNumberDeleteModel>();
            var response = await _villaNumberService.GetAsync<APIResponse>(villaNo);

            if (response != null && response.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<VillaNumberModel>(Convert.ToString(response.Result));
                villaNumberDeleteVM.VillaNumber = _mapper.Map<VillaNumberDeleteModel>(model);
            }

            var responseVilla = await _villaService.GetAllAsync<APIResponse>();

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVillaNumber(VillaNumberDeleteVM model)
        {
            var response = await _villaNumberService.DeleteAsync<APIResponse>(model.VillaNumber.VillaNo);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexVillaNumber));
            }

            return View(model);
        }
    }
}