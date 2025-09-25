using FinancialManagementSystem.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManagementSystem.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISender _sender;

        public LoginController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var response = await _sender.Send(new SignInQuery(email, password));

                // Save session
                HttpContext.Session.SetString("JwtToken", response.JwtToken);
                HttpContext.Session.SetString("RefreshToken", response.RefreshToken);
                HttpContext.Session.SetString("Email", response.Email);
                HttpContext.Session.SetString("FullName", response.FullName);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Add error to ModelState
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login");
        }
    }
}
