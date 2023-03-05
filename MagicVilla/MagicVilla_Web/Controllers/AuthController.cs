using Autofac;
using MagicVilla_Web.Codes;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Security.Claims;

namespace MagicVilla_Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ILifetimeScope _scope;

        public AuthController(ILifetimeScope scope, IAuthService authService)
        {
            _authService = authService;
            _scope = scope;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var loginRequestObj = _scope.Resolve<LoginRequestModel>();
            return View(loginRequestObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            var response = await _authService.LoginAsync<APIResponse>(model);

            if (response != null && response.IsSuccess)
            {
                var loginReponseModel = JsonConvert.DeserializeObject<LoginResponseModel>(Convert.ToString(response.Result));

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, loginReponseModel.User.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, loginReponseModel.User.Role));
                var principle = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);

                HttpContext.Session.SetString(SessionData.SessionToken, loginReponseModel.Token);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("CustomError", response.ErrorMessages.FirstOrDefault());
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var registrationRequestObj = _scope.Resolve<RegistrationRequestModel>();
            return View(registrationRequestObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationRequestModel model)
        {
            var result = await _authService.RegisterAsync<APIResponse>(model);

            if (result != null && result.IsSuccess)
            {
                return RedirectToAction(nameof(Login));
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(SessionData.SessionToken, "");
            return RedirectToAction(nameof(Index), "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}