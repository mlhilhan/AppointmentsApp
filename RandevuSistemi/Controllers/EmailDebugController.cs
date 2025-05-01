using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RandevuSistemi.Core.Services;
using System.Threading.Tasks;

namespace RandevuSistemi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmailDebugController : Controller
    {
        private readonly IEmailService _emailService;

        public EmailDebugController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TestEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                TempData["ErrorMessage"] = "Lütfen geçerli bir e-posta adresi girin.";
                return RedirectToAction("Index");
            }

            try
            {
                await _emailService.SendEmailAsync(
                    email,
                    "Test E-postası",
                    "Bu bir test e-postasıdır. E-posta bildirim sistemi çalışıyor."
                );

                TempData["SuccessMessage"] = "Test e-postası gönderildi. Konsol çıktısını kontrol edin.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "E-posta gönderilirken bir hata oluştu: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}