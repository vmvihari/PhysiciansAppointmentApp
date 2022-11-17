using PhysiciansAppointment.DataAccess.Models;

namespace PhysiciansAppointment.DataAccess.Processors
{
    public interface IDoctorProcessor
    {
        Task<IEnumerable<Doctor>> LoadAllDoctors();
        Doctor? LoadDoctorById(int doctorId);
    }
}