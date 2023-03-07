using Autofac;
using MagicVilla_VillaAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    public class VillaNumberAPIController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly APIResponse _response;

        public VillaNumberAPIController(ILifetimeScope scope)
        {
            _scope = scope;
            _response = _scope.Resolve<APIResponse>();
        }

        [HttpGet("GetString")]
        public object Get()
        {
            return new string[] { "String1", "String2" };
        }

        //[MapToApiVersion("1.0")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<object>> GetVillaNumbers()
        {
            try
            {
                var model = _scope.Resolve<VillaNumberListModel>();
                var villaNumbers = await model.GetAllVillaNumbers();

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.ErrorMessages = new List<string>();
                _response.Result = villaNumbers;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.Message
                };
                _response.Result = null;

                return BadRequest(_response);
            }
        }

        //[MapToApiVersion("2.0")]
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        [HttpGet("{villaNo}")]
        public async Task<object> GetVillaNumber(int villaNo)
        {
            try
            {
                var model = _scope.Resolve<VillaNumberListModel>();
                var villaNumber = await model.GetVillaNumber(villaNo);

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.ErrorMessages = new List<string>();
                _response.Result = villaNumber;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.Message
                };
                _response.Result = null;

                return BadRequest(_response);
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateModel model)
        {
            try
            {
                model.ResolveDependency(_scope);

                await model.CreateVillaNumber();

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.ErrorMessages = new List<string>();
                _response.Result = null;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.Message
                };
                _response.Result = null;

                return BadRequest(_response);
            }
        }

        [HttpDelete("{villaNo}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteVillaNumber(int villaNo)
        {
            try
            {
                var model = _scope.Resolve<VillaNumberListModel>();
                await model.DeleteVillaNumber(villaNo);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.ErrorMessages = new List<string>();
                _response.Result = null;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.Message
                };
                _response.Result = null;

                return BadRequest(_response);
            }
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberEditModel model)
        {
            try
            {
                model.ResolveDependency(_scope);
                await model.EditVillaNumber();

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.ErrorMessages = new List<string>();
                _response.Result = null;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.Message
                };
                _response.Result = null;

                return BadRequest(_response);
            }
        }
    }
}