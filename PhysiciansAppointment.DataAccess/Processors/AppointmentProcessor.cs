using Microsoft.EntityFrameworkCore;
using PhysiciansAppointment.DataAccess.DataAccess;
using PhysiciansAppointment.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PhysiciansAppointment.DataAccess.Processors
{
    public class AppointmentProcessor : IAppointmentProcessor
    {
        private readonly AppointmentDbContext _dbContext;
        private readonly IDoctorProcessor _doctorProcessor;

        public AppointmentProcessor(AppointmentDbContext dbContext, IDoctorProcessor doctorProcessor)
        {
            this._dbContext = dbContext;
            this._doctorProcessor = doctorProcessor;
        }

        public async Task<IEnumerable<Appointment>> LoadDoctorAppointmentsOnDay(int doctorId, DateTime date) => await _dbContext.Appointment
            .Where(appointment => appointment.Doctor.Id == doctorId
            && appointment.AppointmentDateTime.Date == date.Date).ToListAsync();

        public IEnumerable<Appointment> LoadDoctorAppointmentsOnDate(int doctorId, DateTime date) => _dbContext.Appointment
            .Where(appointment => appointment.Doctor.Id == doctorId
            && appointment.AppointmentDateTime.Date == date.Date
            && appointment.AppointmentDateTime.Hour == date.Hour
            && appointment.AppointmentDateTime.Minute == date.Minute);

        public async Task DeleteDoctorAppointment(int appointmentId)
        {
            var appointment = await _dbContext.Appointment.Where(appointment => appointment.Id == appointmentId).FirstOrDefaultAsync();
            if (appointment == null)
            {
                throw new ArgumentException("Appointment not found");
            }
            
            _dbContext.Remove(appointment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveAppointment(Appointment appointment)
        {
            // New appointments can only start at 15 minute intervals
            if (appointment.AppointmentDateTime.Minute % 15 != 0)
            {
                throw new ArgumentException("New appointments can only start at 15 minute intervals");
            }

            // Validate doctor
            if (_doctorProcessor.LoadDoctorById(appointment.DoctorId) == null)
            {
                throw new InvalidOperationException("Doctor is not found");
            }

            //no more than 3 appointments can be added with the same time for a given doctor
            var docAppointments = LoadDoctorAppointmentsOnDate(appointment.DoctorId, appointment.AppointmentDateTime);
            if (docAppointments.Any() && docAppointments.Count() >= 3)
            {
                throw new ArgumentException("No more than 3 appointments can be added with the same time for a given doctor");
            }

            _dbContext.Add(appointment);
            await _dbContext.SaveChangesAsync();
        }
    }
}
