using Autofac;
using MagicVilla_VillaAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers.v2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class VillaNumberAPIController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly APIResponse _response;

        public VillaNumberAPIController(ILifetimeScope scope)
        {
            _scope = scope;
            _response = _scope.Resolve<APIResponse>();
        }

        //[MapToApiVersion("2.0")]
        [HttpGet("GetString")]
        public object Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}