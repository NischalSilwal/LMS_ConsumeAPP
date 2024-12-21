using LMS_ConsumeAPP.Application.DTOs.User;
using LMS_ConsumeAPP.Application.Interface.Services.AuthService;
using LMS_ConsumeAPP.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace LMS_ConsumeAPP.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }
        // GET: /Account/Login
        public IActionResult Login()
        {
            return View(); // Make sure this points to Views/Account/Login.cshtml
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserDto user)
        {
            if (ModelState.IsValid)
            {
                // Authenticate and retrieve the token
                string token = await _authService.AuthenticateAsync(user);

                if (!string.IsNullOrEmpty(token))
                {
                    // Store the token in session
                    HttpContext.Session.SetString("JWToken", token);

                    // Redirect to the dashboard on successful login
                    return RedirectToAction("Index", "Books");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(user);
        }


    }
}
