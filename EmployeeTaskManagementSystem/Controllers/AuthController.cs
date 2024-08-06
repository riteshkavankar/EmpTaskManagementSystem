using Microsoft.AspNetCore.Mvc;
using EmployeeTaskManagementSystem.Models.Dto;
using EmployeeTaskManagementSystem.Service;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace EmployeeTaskManagementSystem.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserLoginDto userLoginDto)
        {
            var loginResponse = await _authService.LoginAsync(userLoginDto);
            if (loginResponse != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userLoginDto.Username),
                    new Claim("EmployeeId", loginResponse.EmployeeId.ToString()),
                    new Claim(ClaimTypes.Role, loginResponse.Role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(userLoginDto);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
    }
}
