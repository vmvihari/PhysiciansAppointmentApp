using Microsoft.EntityFrameworkCore;
using PhysiciansAppointment.DataAccess.DataAccess;
using PhysiciansAppointment.DataAccess.Models;

namespace PhysiciansAppointment.DataAccess.DataSeed
{
    public class AppointmentSeeder : ISeeder
    {
        public async Task SeedAsync(AppointmentDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Appointment.Any())
            {
                return;
            }

            var doctor = await dbContext.Doctor.FirstOrDefaultAsync();

            var appointments = new List<Appointment>()
            {
                new Appointment()
                {
                    PatientFirstName = "Laurel",
                    PatientLastName = "Reitler",
                    AppointmentDateTime = DateTime.Now.AddDays(1),
                    Kind = Kind.NewPatient,
                    DoctorId = doctor.Id,
                },
                new Appointment()
                {
                    PatientFirstName = "Jaclyn",
                    PatientLastName = "Bachman",
                    AppointmentDateTime = DateTime.Now.AddDays(2),
                    Kind = Kind.Followup,
                    DoctorId = doctor.Id,
                },
                new Appointment()
                {
                    PatientFirstName = "Cyril",
                    PatientLastName = "Daufeldt",
                    AppointmentDateTime = DateTime.Now.AddDays(3),
                    Kind = Kind.NewPatient,
                    DoctorId = doctor.Id,
                }
            };

            await dbContext.AddRangeAsync(appointments);
            await dbContext.SaveChangesAsync();
        }
    }
}
