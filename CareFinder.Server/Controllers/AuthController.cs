using CareFinder.Server.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CareFinder.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDTO request)
        {
            var response = await _authService
                .Register(
                    new Doctor
                    {
                        Email = request.Email,
                        FullName = request.FullName,
                        Specialty = request.Specialty,
                        PracticeName = request.PracticeName,
                        City = request.City,
                        Qualifications = request.Qualifications,
                        About = request.About,
                    },
                    request.Password
                );

            return response.Success ? Ok(response) : BadRequest();
        }
    }
}
