using Microsoft.EntityFrameworkCore;
using RandevuSistemi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandevuSistemi.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Appointments)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "admin",
                    Password = "admin123",
                    Email = "admin@example.com",
                    FullName = "System Admin",
                    IsAdmin = true
                },
                new User
                {
                    Id = 2,
                    UserName = "user",
                    Password = "user123",
                    Email = "user@example.com",
                    FullName = "System User",
                    IsAdmin = false
                }
            );
        }
    }
}
