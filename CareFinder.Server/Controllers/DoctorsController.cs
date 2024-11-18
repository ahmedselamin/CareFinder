using Microsoft.AspNetCore.Mvc;

namespace CareFinder.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet("/{doctorId:int}")]
        public async Task<ActionResult> GetDoctor(int doctorId)
        {
            var response = await _doctorService.GetDoctor(doctorId);

            return response.Success ? Ok(response) : BadRequest(response.Message);
        }
    }
}
