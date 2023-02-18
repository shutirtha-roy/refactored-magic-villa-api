using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberService _villaNumberService;
        private readonly IMapper _mapper;

        public VillaNumberController(IVillaNumberService villaNumberService, IMapper mapper)
        {
            _villaNumberService = villaNumberService;
            _mapper = mapper;
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
    }
}