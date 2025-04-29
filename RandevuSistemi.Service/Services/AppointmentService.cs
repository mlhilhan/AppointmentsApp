using RandevuSistemi.Core.Entities;
using RandevuSistemi.Core.Repositories;
using RandevuSistemi.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandevuSistemi.Service.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IRepository<Appointment> _appointmentRepository;

        public AppointmentService(IRepository<Appointment> appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            return await _appointmentRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Appointment>> GetUserAppointmentsAsync(int userId)
        {
            return await _appointmentRepository.FindAsync(a => a.UserId == userId && !a.IsDeleted);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDateAsync(DateTime date)
        {
            return await _appointmentRepository.FindAsync(a =>
                a.AppointmentDate.Date == date.Date && !a.IsDeleted);
        }

        public async Task<IEnumerable<Appointment>> GetFutureAppointmentsAsync(int userId)
        {
            return await _appointmentRepository.FindAsync(a =>
                a.UserId == userId &&
                (a.AppointmentDate > DateTime.Today ||
                (a.AppointmentDate == DateTime.Today && a.AppointmentTime > DateTime.Now.TimeOfDay)) &&
                !a.IsDeleted);
        }

        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
            await _appointmentRepository.AddAsync(appointment);
            return appointment;
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            await _appointmentRepository.UpdateAsync(appointment);
        }

        public async Task DeleteAppointmentAsync(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment != null)
            {
                appointment.IsDeleted = true;
                await _appointmentRepository.UpdateAsync(appointment);
            }
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _appointmentRepository.GetAllWithIncludeAsync(a => a.User);
        }
    }
}
