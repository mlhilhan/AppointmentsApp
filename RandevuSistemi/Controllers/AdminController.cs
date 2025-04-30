using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RandevuSistemi.Core.Services;
using System.Threading.Tasks;

namespace RandevuSistemi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAppointmentService _appointmentService;

        public AdminController(IUserService userService, IAppointmentService appointmentService)
        {
            _userService = userService;
            _appointmentService = appointmentService;
        }

        // Admin Dashboard
        public IActionResult Index()
        {
            return View();
        }

        // Kullanıcı Listesi
        public async Task<IActionResult> Users()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        // Tüm Randevular
        public async Task<IActionResult> AllAppointments()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return View(appointments);
        }
    }
}