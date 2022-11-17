using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhysiciansAppointment.DataAccess.Models;
using PhysiciansAppointment.DataAccess.Processors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhysiciansAppointment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorProcessor _doctorProcessor;
        private readonly ILogger<DoctorsController> _logger;

        public DoctorsController(IDoctorProcessor doctorProcessor, ILogger<DoctorsController> logger)
        {
            this._doctorProcessor = doctorProcessor;
            this._logger = logger;
        }

        // GET: api/<DoctorsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> Get()
        {
            try
            {
                var doctors = await _doctorProcessor.LoadAllDoctors();
                return Ok(doctors);
            }
            catch (Exception e)
            {
                _logger.LogError("Exception occured while processing request: ", e.Message);
                return StatusCode(500);
            }
        }
    }
}
