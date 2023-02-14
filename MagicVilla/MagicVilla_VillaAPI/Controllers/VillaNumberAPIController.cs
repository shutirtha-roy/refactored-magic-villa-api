using Autofac;
using MagicVilla_VillaAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaNumberAPIController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly APIResponse _response;

        public VillaNumberAPIController(ILifetimeScope scope)
        {
            _scope = scope;
            _response = _scope.Resolve<APIResponse>();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}