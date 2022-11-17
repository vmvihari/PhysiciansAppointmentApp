using Microsoft.AspNetCore.Mvc;
using PhysiciansAppointment.DataAccess.Models;
using PhysiciansAppointment.DataAccess.Processors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhysiciansAppointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentProcessor _appointmentProcessor;
        private readonly ILogger<AppointmentsController> _logger;

        public AppointmentsController(IAppointmentProcessor appointmentProcessor, ILogger<AppointmentsController> logger)
        {
            this._appointmentProcessor = appointmentProcessor;
            this._logger = logger;
        }

        // GET: api/<AppointmentsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetDoctorAppointments(int doctorId, string date)
        {
            DateTime appointmentDate;
            if (DateTime.TryParse(date, out appointmentDate))
            {
                var doctorAppointments = await _appointmentProcessor.LoadDoctorAppointmentsOnDay(doctorId, appointmentDate);
                return Ok(doctorAppointments);
            }

            return NotFound();
        }

        // POST api/<AppointmentsController>
        [HttpPost]
        public async Task<ActionResult> SaveAppointment([FromBody] Appointment appointment)
        {
            try
            {
                await _appointmentProcessor.SaveAppointment(appointment);
                return Ok();
            }
            catch (ArgumentException argExp)
            {
                _logger.LogError("Invalid argument exception occured: ", argExp.Message);
                return BadRequest(argExp.Message);
            }
            catch (InvalidOperationException oprExp)
            {
                _logger.LogError("Invalid operation exception occured: ", oprExp.Message);
                return BadRequest(oprExp.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("Exception occured while processing request: ", e.Message);
                return StatusCode(500);
            }
        }

        // DELETE api/<AppointmentsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> CancelAppointment(int id)
        {
            try
            {
                await _appointmentProcessor.DeleteDoctorAppointment(id);
                return Ok();
            }
            catch(ArgumentException argExp)
            {
                _logger.LogError("Invalid argument exception occured: ", argExp.Message);
                return BadRequest(argExp.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("Exception occured while processing request: ", e.Message);
                return StatusCode(500);
            }
        }
    }
}
