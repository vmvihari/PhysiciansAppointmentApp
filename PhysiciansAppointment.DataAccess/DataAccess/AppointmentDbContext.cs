using Microsoft.EntityFrameworkCore;
using PhysiciansAppointment.DataAccess.Models;

namespace PhysiciansAppointment.DataAccess.DataAccess
{
    public class AppointmentDbContext : DbContext
    {
        public AppointmentDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
    }
}
