using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpGet("{doctorId:int}")]
        public async Task<ActionResult> GetDoctor(int doctorId)
        {
            var response = await _doctorService.GetDoctor(doctorId);

            return response.Success ? Ok(response) : BadRequest(response.Message);
        }

        [HttpGet("search/{searchText}")]
        public async Task<ActionResult> SearchDoctors(string searchText)
        {
            var response = await _doctorService.SearchDoctors(searchText);

            return response.Success ? Ok(response) : BadRequest(response.Message);
        }

        [HttpGet("fetch-slots/{doctorId:int}")]
        public async Task<ActionResult> FetchSlots(int doctorId)
        {
            var response = await _doctorService.GetAvailabilitySlots(doctorId);

            return response.Success ? Ok(response) : BadRequest(response.Message);
        }

        [HttpPost("add-slot"), Authorize]
        public async Task<ActionResult> AddNewSlot([FromBody] SlotDTO request)
        {
            var doctorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var slot = new AvailabilitySlot
            {
                DoctorId = doctorId,
                Day = request.Day,
                StartsAt = request.StartAt,
                EndsAt = request.EndsAt,
            };

            var response = await _doctorService.AddAvailabilitySlot(doctorId, slot);

            return response.Success ? Ok(response) : BadRequest(response.Message);
        }
    }
}
