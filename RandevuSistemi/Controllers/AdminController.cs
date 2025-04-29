using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RandevuSistemi.Core.Entities;
using RandevuSistemi.Core.Services;
using System.Threading.Tasks;

namespace RandevuSistemi.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAppointmentService _appointmentService;

        public AdminController(IUserService userService, IAppointmentService appointmentService)
        {
            _userService = userService;
            _appointmentService = appointmentService;
        }

        // Yönetici erişimi kontrolü
        private bool IsAdmin()
        {
            return HttpContext.Session.GetInt32("IsAdmin") == 1;
        }

        // Yönetici kontrolü ve yönlendirme
        private IActionResult CheckAdmin()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (!IsAdmin())
            {
                TempData["ErrorMessage"] = "Bu sayfaya erişim yetkiniz bulunmamaktadır.";
                return RedirectToAction("Index", "Home");
            }

            return null;
        }

        // Admin Dashboard
        public IActionResult Index()
        {
            var checkResult = CheckAdmin();
            if (checkResult != null)
                return checkResult;

            return View();
        }

        // Kullanıcı Listesi
        public async Task<IActionResult> Users()
        {
            var checkResult = CheckAdmin();
            if (checkResult != null)
                return checkResult;

            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        // Tüm Randevular
        public async Task<IActionResult> AllAppointments()
        {
            var checkResult = CheckAdmin();
            if (checkResult != null)
                return checkResult;

            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return View(appointments);
        }
    }
}