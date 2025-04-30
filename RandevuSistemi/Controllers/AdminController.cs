using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RandevuSistemi.Core.Entities;
using RandevuSistemi.Core.Services;
using RandevuSistemi.Models;
using System;
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

        // Randevu Düzenleme Sayfası
        public async Task<IActionResult> Edit(int id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
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

            return View("~/Views/Appointment/Edit.cshtml", model);
        }

        // Randevu Düzenleme Post İşlemi
        [HttpPost]
        public async Task<IActionResult> Edit(AppointmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var appointment = await _appointmentService.GetAppointmentByIdAsync(model.Id);
                if (appointment == null)
                {
                    return NotFound();
                }

                appointment.Title = model.Title;
                appointment.Description = model.Description;
                appointment.AppointmentDate = model.AppointmentDate;
                appointment.AppointmentTime = model.AppointmentTime;

                await _appointmentService.UpdateAppointmentAsync(appointment);
                TempData["SuccessMessage"] = "Randevu başarıyla güncellendi.";
                return RedirectToAction("AllAppointments");
            }

            return View("~/Views/Appointment/Edit.cshtml", model);
        }

        // Randevu Silme Onay Sayfası
        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View("~/Views/Appointment/Delete.cshtml", appointment);
        }

        // Randevu Silme Post İşlemi
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointmentToDelete = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointmentToDelete == null)
            {
                return NotFound();
            }

            await _appointmentService.DeleteAppointmentAsync(id);
            TempData["SuccessMessage"] = "Randevu başarıyla silindi.";
            return RedirectToAction("AllAppointments");
        }

        // Randevu Geri Yükleme İşlemi (Silinen randevuları geri getirme)
        [HttpPost]
        public async Task<IActionResult> RestoreAppointment(int id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            appointment.IsDeleted = false;
            await _appointmentService.UpdateAppointmentAsync(appointment);
            TempData["SuccessMessage"] = "Randevu başarıyla geri yüklendi.";
            return RedirectToAction("AllAppointments");
        }
    }
}