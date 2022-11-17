using PhysiciansAppointment.DataAccess.DataAccess;

namespace PhysiciansAppointment.DataAccess.DataSeed
{
    public interface ISeeder
    {
        Task SeedAsync(AppointmentDbContext dbContext, IServiceProvider serviceProvider);
    }
}