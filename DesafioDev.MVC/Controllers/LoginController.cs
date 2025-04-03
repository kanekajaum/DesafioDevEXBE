using DesafioDev.MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DesafioDev.MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly AuthService _authService;

        public LoginController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            string isValid = await _authService.ValidateUserAsync(request);

            if (isValid != null)
            {
                HttpContext.Session.SetString("AuthToken", isValid);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["ErrorMessage"] = "E-mail ou senha inválidos.";
                return RedirectToAction("Login", "Home");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("AuthToken");
            return RedirectToAction("Login", "Home");
        }
    }
}