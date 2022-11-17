using Microsoft.EntityFrameworkCore;
using PhysiciansAppointment.DataAccess.DataAccess;
using PhysiciansAppointment.DataAccess.DataSeed;
using PhysiciansAppointment.DataAccess.Processors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DB context
builder.Services.AddDbContext<AppointmentDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddTransient<IDoctorProcessor, DoctorProcessor>();
builder.Services.AddTransient<IAppointmentProcessor, AppointmentProcessor>();


var app = builder.Build();

// Seed data on application startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppointmentDbContext>();
    dbContext.Database.Migrate();
    AppointmentDbContextSeeder.Initialize(dbContext, scope.ServiceProvider).GetAwaiter().GetResult();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
