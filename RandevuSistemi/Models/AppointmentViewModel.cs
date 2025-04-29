using System;
using System.ComponentModel.DataAnnotations;

namespace RandevuSistemi.Models
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık gereklidir")]
        [Display(Name = "Başlık")]
        public string Title { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Tarih gereklidir")]
        [DataType(DataType.Date)]
        [Display(Name = "Tarih")]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "Saat gereklidir")]
        [DataType(DataType.Time)]
        [Display(Name = "Saat")]
        public TimeSpan AppointmentTime { get; set; }
    }
}