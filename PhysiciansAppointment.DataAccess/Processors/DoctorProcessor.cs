using Microsoft.EntityFrameworkCore;
using PhysiciansAppointment.DataAccess.DataAccess;
using PhysiciansAppointment.DataAccess.Models;

namespace PhysiciansAppointment.DataAccess.Processors
{
    public class DoctorProcessor : IDoctorProcessor
    {
        private readonly AppointmentDbContext _dbContext;

        public DoctorProcessor(AppointmentDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<Doctor>> LoadAllDoctors() => await _dbContext.Doctor.ToListAsync();

        public Doctor? LoadDoctorById(int doctorId) => _dbContext.Doctor.Where(doctor => doctor.Id == doctorId).FirstOrDefault();
    }
}
