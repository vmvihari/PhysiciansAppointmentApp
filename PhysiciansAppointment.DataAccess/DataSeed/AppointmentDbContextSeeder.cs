using PhysiciansAppointment.DataAccess.DataAccess;

namespace PhysiciansAppointment.DataAccess.DataSeed
{
    public class AppointmentDbContextSeeder
    {
        public static async Task Initialize(AppointmentDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var seeders = new List<ISeeder>
            {
                new DoctorSeeder(),
                new AppointmentSeeder()
            };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, serviceProvider);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
