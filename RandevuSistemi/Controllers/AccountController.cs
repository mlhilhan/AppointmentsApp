using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RandevuSistemi.Core.Entities;
using RandevuSistemi.Core.Services;
using RandevuSistemi.Models;
using System.Threading.Tasks;

namespace RandevuSistemi.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isValid = await _userService.ValidateUserAsync(model.Username, model.Password);
                if (isValid)
                {
                    var user = await _userService.GetUserByUsernameAsync(model.Username);
                    HttpContext.Session.SetInt32("UserId", user.Id);
                    HttpContext.Session.SetString("Username", user.UserName);
                    HttpContext.Session.SetString("FullName", user.FullName);
                    HttpContext.Session.SetInt32("IsAdmin", user.IsAdmin ? 1 : 0);

                    return RedirectToAction("Index", "Appointment");
                }
                ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış");
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userService.GetUserByUsernameAsync(model.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Username", "Bu kullanıcı adı zaten kullanılıyor");
                    return View(model);
                }

                var user = new User
                {
                    UserName = model.Username,
                    Password = model.Password,
                    Email = model.Email,
                    FullName = model.FullName,
                    IsAdmin = false
                };

                await _userService.RegisterUserAsync(user);
                TempData["SuccessMessage"] = "Kayıt başarılı! Şimdi giriş yapabilirsiniz.";
                return RedirectToAction("Login");
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}