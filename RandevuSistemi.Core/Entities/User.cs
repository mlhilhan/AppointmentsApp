﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandevuSistemi.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string? Password { get; set; } // Eski alan - Geçiş sürecinde korunacak
        public string? PasswordHash { get; set; } // Yeni eklenen alan
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool IsAdmin { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
