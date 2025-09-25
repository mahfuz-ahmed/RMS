using FinancialManagementSystem.Application;
using FinancialManagementSystem.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public class SignUpController : Controller
{
    private readonly ISender _sender;
    public SignUpController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(string firstName, string lastName, string email, string password)
    {
        try
        {
            var user = new User
            {
                FullName = $"{firstName} {lastName}",
                Email = email,
                PasswordHash = password,
                Role = "Admin",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _sender.Send(new SignUpCommand(user));

            return RedirectToAction("Login", "Login");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View();
        }
    }
}
