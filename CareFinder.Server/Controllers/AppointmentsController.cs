using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CareFinder.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost("create-appointment")]
        public async Task<ActionResult> CreateAppointment([FromBody] CreateAppointmentDTO request)
        {
            var appointmentDetails = new Appointment
            {
                DoctorId = request.DoctorId,
                TimeSlotId = request.TimeSlotId,
                PatientFullName = request.PatientsFullName
            };

            var response = await _appointmentService.CreateAppointment(appointmentDetails);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("fetch-appointments/"), Authorize]
        public async Task<ActionResult> FetchAllAppointments()
        {
            var doctorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var response = await _appointmentService.FetchAllAppointments(doctorId);

            return response.Success ? Ok(response) : BadRequest(response);

        }
    }
}
