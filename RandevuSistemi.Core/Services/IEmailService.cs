using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandevuSistemi.Core.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
        Task SendAppointmentConfirmationAsync(string to, string fullName, string appointmentTitle, string date, string time);
        Task SendAppointmentCancellationAsync(string to, string fullName, string appointmentTitle, string date, string time);
        Task SendAppointmentUpdateAsync(string to, string fullName, string appointmentTitle, string date, string time);
    }
}
