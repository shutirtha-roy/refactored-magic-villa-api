using Autofac;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Model;
using MagicVilla_VillaAPI.Model.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILifetimeScope _scope;

        public VillaAPIController(ILifetimeScope scope)
        {
            _scope = scope;
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
        public async Task<ActionResult<object>> GetVillas()
        {
            try
            {
                var model = _scope.Resolve<VillaListModel>();
                var villas = await model.GetAllVillas();

                return Ok(villas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
        public async Task<object> GetVilla(int id)
        {
            try
            {
                var model = _scope.Resolve<VillaListModel>();
                var villa = await model.GetVilla(id);

                return Ok(villa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
        public async Task<IActionResult> CreateVilla(VillaCreateModel model)
        {
            try
            {
                model.ResolveDependency(_scope);

                await model.CreateVilla();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
        public async Task<IActionResult> DeleteVilla(int id)
        {
            try
            {
                var model = _scope.Resolve<VillaListModel>();
                await model.DeleteVilla(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
        public async Task<IActionResult> UpdateVilla(VillaEditModel model)
        {
            try
            {
                model.ResolveDependency(_scope);
                await model.EditVilla();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
                return BadRequest();

            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);

            if (villa == null)
                return BadRequest();

            patchDTO.ApplyTo(villa, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return NoContent();
        }
    }
}