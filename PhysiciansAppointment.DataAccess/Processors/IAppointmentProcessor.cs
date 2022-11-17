using PhysiciansAppointment.DataAccess.Models;

namespace PhysiciansAppointment.DataAccess.Processors
{
    public interface IAppointmentProcessor
    {
        Task<IEnumerable<Appointment>> LoadDoctorAppointmentsOnDay(int doctorId, DateTime date);
        IEnumerable<Appointment> LoadDoctorAppointmentsOnDate(int doctorId, DateTime date);
        Task DeleteDoctorAppointment(int appointmentId);
        Task SaveAppointment(Appointment appointment);
    }
}