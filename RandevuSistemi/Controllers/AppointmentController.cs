using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RandevuSistemi.Core.Entities;
using RandevuSistemi.Core.Services;
using RandevuSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RandevuSistemi.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IUserService _userService;

        public AppointmentController(IAppointmentService appointmentService, IUserService userService)
        {
            _appointmentService = appointmentService;
            _userService = userService;
        }

        // Kullanıcı ID'sini claim'den alan helper metodu
        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return -1;
            }
            return userId;
        }

        public async Task<IActionResult> Index()
        {
            int userId = GetCurrentUserId();
            if (userId == -1)
            {
                return RedirectToAction("Login", "Account");
            }

            var appointments = await _appointmentService.GetUserAppointmentsAsync(userId);
            return View(appointments);
        }

        public IActionResult Create()
        {
            int userId = GetCurrentUserId();
            if (userId == -1)
            {
                return RedirectToAction("Login", "Account");
            }

            var model = new AppointmentViewModel
            {
                AppointmentDate = DateTime.Today,
                AppointmentTime = DateTime.Now.TimeOfDay
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppointmentViewModel model)
        {
            int userId = GetCurrentUserId();
            if (userId == -1)
            {
                return RedirectToAction("Login", "Account");
            }

            if (model.AppointmentDate.Date < DateTime.Today ||
                (model.AppointmentDate.Date == DateTime.Today && model.AppointmentTime < DateTime.Now.TimeOfDay))
            {
                ModelState.AddModelError("AppointmentDate", "Geçmiş tarih veya saate randevu oluşturulamaz.");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                var appointment = new Appointment
                {
                    UserId = userId,
                    Title = model.Title,
                    Description = model.Description,
                    AppointmentDate = model.AppointmentDate,
                    AppointmentTime = model.AppointmentTime,
                    IsDeleted = false
                };

                await _appointmentService.CreateAppointmentAsync(appointment);
                TempData["SuccessMessage"] = "Randevu başarıyla oluşturuldu.";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            int userId = GetCurrentUserId();
            if (userId == -1)
            {
                return RedirectToAction("Login", "Account");
            }

            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null || appointment.UserId != userId)
            {
                return NotFound();
            }

            var model = new AppointmentViewModel
            {
                Id = appointment.Id,
                Title = appointment.Title,
                Description = appointment.Description,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AppointmentViewModel model)
        {
            int userId = GetCurrentUserId();
            if (userId == -1)
            {
                return RedirectToAction("Login", "Account");
            }

            if (model.AppointmentDate.Date < DateTime.Today ||
                (model.AppointmentDate.Date == DateTime.Today && model.AppointmentTime < DateTime.Now.TimeOfDay))
            {
                ModelState.AddModelError("AppointmentDate", "Geçmiş tarih veya saate randevu düzenlenemez.");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                var appointment = await _appointmentService.GetAppointmentByIdAsync(model.Id);
                if (appointment == null || appointment.UserId != userId)
                {
                    return NotFound();
                }

                appointment.Title = model.Title;
                appointment.Description = model.Description;
                appointment.AppointmentDate = model.AppointmentDate;
                appointment.AppointmentTime = model.AppointmentTime;

                await _appointmentService.UpdateAppointmentAsync(appointment);
                TempData["SuccessMessage"] = "Randevu başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            int userId = GetCurrentUserId();
            if (userId == -1)
            {
                return RedirectToAction("Login", "Account");
            }

            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null || appointment.UserId != userId)
            {
                return NotFound();
            }

            return View(appointment);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int userId = GetCurrentUserId();
            if (userId == -1)
            {
                return RedirectToAction("Login", "Account");
            }

            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null || appointment.UserId != userId)
            {
                return NotFound();
            }

            await _appointmentService.DeleteAppointmentAsync(id);
            TempData["SuccessMessage"] = "Randevu başarıyla silindi.";
            return RedirectToAction("Index");
        }
    }
}