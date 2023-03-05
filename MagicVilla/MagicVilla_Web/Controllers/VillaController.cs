using AutoMapper;
using MagicVilla_Web.Codes;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public VillaController(IVillaService villaService, IMapper mapper)
        {
            _villaService = villaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexVilla()
        {
            var villaList = new List<VillaModel>();
            var token = HttpContext.Session.GetString(SessionData.SessionToken);
            var response = await _villaService.GetAllAsync<APIResponse>(token);

            if (response != null && response.IsSuccess)
            {
                villaList = JsonConvert.DeserializeObject<List<VillaModel>>(Convert.ToString(response.Result));
            }

            return View(villaList);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateVilla()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla(VillaCreateModel model)
        {
            var token = HttpContext.Session.GetString(SessionData.SessionToken);
            var response = await _villaService.CreateAsync<APIResponse>(model, token);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Villa created successfully";
                return RedirectToAction(nameof(IndexVilla));
            }

            TempData["error"] = "Error encountered.";
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateVilla(int villaId)
        {
            var token = HttpContext.Session.GetString(SessionData.SessionToken);
            var response = await _villaService.GetAsync<APIResponse>(villaId, token);

            if (response != null && response.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<VillaModel>(Convert.ToString(response.Result));
                return View(_mapper.Map<VillaEditModel>(model));
            }

            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilla(VillaEditModel model)
        {
            if (ModelState.IsValid)
            {
                var token = HttpContext.Session.GetString(SessionData.SessionToken);
                var response = await _villaService.UpdateAsync<APIResponse>(model, token);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa updated successfully";
                    return RedirectToAction(nameof(IndexVilla));
                }
            }

            TempData["error"] = "Error encountered.";
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteVilla(int villaId)
        {
            var token = HttpContext.Session.GetString(SessionData.SessionToken);
            var response = await _villaService.GetAsync<APIResponse>(villaId, token);

            if (response != null && response.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<VillaModel>(Convert.ToString(response.Result));
                return View(model);
            }

            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVilla(VillaModel model)
        {
            var token = HttpContext.Session.GetString(SessionData.SessionToken);
            var response = await _villaService.DeleteAsync<APIResponse>(model.Id, token);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Villa deleted successfully";
                return RedirectToAction(nameof(IndexVilla));
            }

            TempData["error"] = "Error encountered.";
            return View(model);
        }
    }
}