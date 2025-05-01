using Microsoft.Extensions.Logging;
using RandevuSistemi.Core.Services;
using System;
using System.Threading.Tasks;

namespace RandevuSistemi.Service.Services
{
    public class MockEmailService : IEmailService
    {
        private readonly ILogger<MockEmailService> _logger;

        public MockEmailService(ILogger<MockEmailService> logger)
        {
            _logger = logger;
        }

        public Task SendEmailAsync(string to, string subject, string body)
        {
            _logger.LogInformation($"E-posta gönderildi: Kime: {to}, Konu: {subject}");
            _logger.LogInformation($"İçerik: {body}");

            Console.WriteLine($"E-POSTA GÖNDERİLDİ: Kime: {to}");
            Console.WriteLine($"Konu: {subject}");
            Console.WriteLine($"İçerik: {body}");

            return Task.CompletedTask;
        }

        public Task SendAppointmentConfirmationAsync(string to, string fullName, string appointmentTitle, string date, string time)
        {
            string subject = "Randevu Onayı";
            string body = $@"
                Sayın {fullName},

                Randevu talebiniz başarıyla oluşturulmuştur.

                Randevu Bilgileri:
                Başlık: {appointmentTitle}
                Tarih: {date}
                Saat: {time}

                Randevunuz için teşekkür ederiz.
                Randevu Sistemi
                ";

            return SendEmailAsync(to, subject, body);
        }

        public Task SendAppointmentCancellationAsync(string to, string fullName, string appointmentTitle, string date, string time)
        {
            string subject = "Randevu İptali";
            string body = $@"
                Sayın {fullName},

                Aşağıdaki randevunuz iptal edilmiştir.

                Randevu Bilgileri:
                Başlık: {appointmentTitle}
                Tarih: {date}
                Saat: {time}

                İhtiyaç duymanız halinde yeni bir randevu oluşturabilirsiniz.
                Randevu Sistemi
                ";

            return SendEmailAsync(to, subject, body);
        }

        public Task SendAppointmentUpdateAsync(string to, string fullName, string appointmentTitle, string date, string time)
        {
            string subject = "Randevu Güncellemesi";
            string body = $@"
                Sayın {fullName},

                Randevunuz güncellenmiştir.

                Randevu Bilgileri:
                Başlık: {appointmentTitle}
                Tarih: {date}
                Saat: {time}

                Güncellenen randevunuz için teşekkür ederiz.
                Randevu Sistemi
                ";

            return SendEmailAsync(to, subject, body);
        }
    }
}