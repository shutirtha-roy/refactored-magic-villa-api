using Autofac;
using AutoMapper;
using Azure;
using MagicVilla_Infrastructure.BusinessObjects;
using MagicVilla_Infrastructure.Services;
using MagicVilla_VillaAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/UsersAuth")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly APIResponse _response;
        private readonly ILifetimeScope _scope;

        public UsersController(ILifetimeScope scope, IUserService userService, IMapper mapper)
        {
            _scope = scope;
            _userService = userService;
            _mapper = mapper;
            _response = _scope.Resolve<APIResponse>();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            var loginReponse = await _userService.Login(_mapper.Map<Login>(model));
            if (loginReponse.User == null || string.IsNullOrEmpty(loginReponse.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.Result = null;
                _response.ErrorMessages.Add("User or password is incorrect.");
                return BadRequest(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginReponse;
            return Ok(_response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationRequestModel model)
        {
            var ifUserNameUnique = await _userService.IsUniqueUser(model.UserName);
            if(!ifUserNameUnique)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username already exists");
                return BadRequest(_response);
            }

            var user = await _userService.Register(_mapper.Map<Registration>(model));
            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Error while registering");
                return BadRequest(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);
        }
    }
}