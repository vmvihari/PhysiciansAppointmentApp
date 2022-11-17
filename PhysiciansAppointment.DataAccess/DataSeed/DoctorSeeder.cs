using PhysiciansAppointment.DataAccess.DataAccess;
using PhysiciansAppointment.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PhysiciansAppointment.DataAccess.DataSeed
{
    public class DoctorSeeder : ISeeder
    {
        public async Task SeedAsync(AppointmentDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Doctor.Any())
            {
                return;
            }

            var doctors = new List<Doctor>()
            {
                new Doctor
                {
                    FirstName = "Franklyn",
                    LastName = "Emard"
                },
                new Doctor
                {
                    FirstName = "Brittni",
                    LastName = "Gillaspie"
                },
                new Doctor
                {
                    FirstName = "Chauncey",
                    LastName = "Motley"
                },
            };

            await dbContext.AddRangeAsync(doctors);
            await dbContext.SaveChangesAsync();
        }
    }
}
