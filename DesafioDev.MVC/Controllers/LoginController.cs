using DesafioDev.MVC.Models;
using DesafioDev.MVC.Services;
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
                ViewData["Logado"] = "Usuario logado";
                HttpContext.Session.SetString("AuthToken", isValid);
                HttpContext.Session.SetString("EmailToken", request.Email);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["ErrorMessage"] = "E-mail ou senha inválidos.";
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(LoginRequest request)
        {
            string usuario = await _authService.RegistrarUserAsync(request);

            if (usuario != null)
            {
                ViewData["Registrado"] = "Usuario Cadastrado com sucesso";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["ErrorMessage"] = "Erro ao efetuar o cadastro, tente novamente mais tarde.";
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("AuthToken");
            return RedirectToAction("Login", "Home");
        }
    }
}