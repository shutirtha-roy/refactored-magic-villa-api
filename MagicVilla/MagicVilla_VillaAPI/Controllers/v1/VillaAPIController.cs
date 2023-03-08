using Autofac;
using MagicVilla_VillaAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace MagicVilla_VillaAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILifetimeScope _scope;
        private readonly APIResponse _response;

        public VillaAPIController(ILifetimeScope scope)
        {
            _scope = scope;
            _response = _scope.Resolve<APIResponse>();
        }

        #region Previous GetVillasCode
        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        //{
        //    return Ok(VillaStore.villaList);
        //}
        #endregion

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ResponseCache(Duration = 30)]
        [ResponseCache(CacheProfileName = "Default30")]
        public async Task<ActionResult<object>> GetVillas([FromQuery(Name = "filterOccupancy")]int? occupancy,
            [FromQuery]string? search, int pageSize = 0, int pageNumber = 1)
        {
            try
            {
                var model = _scope.Resolve<VillaListModel>();
                //var villas = await model.GetAllVillas();
                var villas = await model.GetAllVillasByPage(pageSize, pageNumber);

                if (occupancy > 0)
                {
                    villas = villas.Where(o => o.Occupancy == occupancy).ToList();
                }

                if (!string.IsNullOrEmpty(search))
                {
                    villas = villas.Where(u => u.Name.ToLower().Contains(search)).ToList();
                }

                var pagination = _scope.Resolve<PaginationModel>();
                pagination.PageNumber = pageNumber;
                pagination.PageSize = pageSize;

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.ErrorMessages = new List<string>();
                _response.Result = villas;

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

        #region PreviousGetVillaCode
        //[HttpGet("{id:int}", Name = "GetVilla")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        ////[ProducesResponseType(200, Type = typeof(VillaDTO)]
        //public ActionResult<VillaDTO> GetVilla(int id)
        //{
        //    if (id == 0)
        //        return BadRequest();

        //    var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);

        //    if(villa == null)
        //        return NotFound();

        //    return Ok(villa);
        //}
        #endregion

        [HttpGet("{id}")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<object> GetVilla(int id)
        {
            try
            {
                var model = _scope.Resolve<VillaListModel>();
                var villa = await model.GetVilla(id);

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.ErrorMessages = new List<string>();
                _response.Result = villa;

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

        #region Previous CreateVillaCode
        //[HttpPost]
        ////[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villaDTO)
        //{
        //    //if(!ModelState.IsValid)
        //    //{
        //    //    return BadRequest(ModelState);
        //    //}
        //    if(VillaStore.villaList.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
        //    {
        //        ModelState.AddModelError("CustomError", "Villa Exists!");
        //        return BadRequest(ModelState);
        //    }

        //    if (villaDTO == null)
        //    {
        //        return BadRequest(villaDTO);
        //    }

        //    if (villaDTO.Id > 0)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }

        //    villaDTO.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
        //    VillaStore.villaList.Add(villaDTO);

        //    //return Ok(villaDTO);
        //    return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
        //}
        #endregion

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateVilla(VillaCreateModel model)
        {
            try
            {
                model.ResolveDependency(_scope);

                await model.CreateVilla();

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

        #region Previous DeleteVilla Code
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[HttpDelete("{id:int}", Name = "DeleteVilla")]
        //public IActionResult DeleteVilla(int id)
        //{
        //    if (id == 0)
        //        return BadRequest();

        //    var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
        //    if (villa == null)
        //        return NotFound();

        //    VillaStore.villaList.Remove(villa);
        //    return NoContent();
        //}
        #endregion

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            try
            {
                var model = _scope.Resolve<VillaListModel>();
                await model.DeleteVilla(id);

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

        #region UpdateVilla Code
        //[HttpPut("{id:int}", Name = "UpdateVilla")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult UpdateVilla(int id, [FromBody]VillaDTO villaDTO)
        //{
        //    if (villaDTO == null || id != villaDTO.Id)
        //        return BadRequest();

        //    var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
        //    villa.Name = villaDTO.Name;
        //    villa.Sqft = villaDTO.Sqft;
        //    villa.Occupancy = villaDTO.Occupancy;

        //    return NoContent();
        //}
        #endregion

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateVilla(VillaEditModel model)
        {
            try
            {
                model.ResolveDependency(_scope);
                await model.EditVilla();

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

        #region Previous Patch Code
        //[HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        //{
        //    if (patchDTO == null || id == 0)
        //        return BadRequest();

        //    var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);

        //    if (villa == null)
        //        return BadRequest();

        //    patchDTO.ApplyTo(villa, ModelState);

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    return NoContent();
        //}
        #endregion

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaEditModel> patchDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var model = _scope.Resolve<VillaEditModel>();
                var villa = await model.GetVilla(id);
                patchDTO.ApplyTo(villa, ModelState);
                villa.ResolveDependency(_scope);
                await villa.EditVilla();

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