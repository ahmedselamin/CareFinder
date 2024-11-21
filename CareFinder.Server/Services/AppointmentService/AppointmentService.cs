namespace CareFinder.Server.Services.AppointmentService
{
    public class AppointmentService : IAppointmentService
    {
        private readonly DataContext _context;

        public AppointmentService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<bool>> CreateAppointment(Appointment appointment)
        {
            var response = new ServiceResponse<bool>();

            try
            {

                if (appointment.TimeSlotId == 0 || appointment.DoctorId == 0 || appointment.PatientFullName == null)
                {
                    response.Success = false;
                    response.Message = "All fields must be filled";

                    return response;
                }

                var doctor = await _context.Doctors.FindAsync(appointment.DoctorId);
                if (doctor == null)
                {
                    response.Success = false;
                    response.Message = "Doctor not found.";
                    return response;
                }

                var timeSlot = await _context.AvailabilitySlots.FindAsync(appointment.TimeSlotId);
                if (timeSlot == null)
                {
                    response.Success = false;
                    response.Message = "Time slot not found.";
                    return response;
                }

                if (timeSlot.DoctorId != doctor.Id)
                {
                    response.Success = false;
                    response.Message = "Doctor and time slot do not match.";

                    return response;
                }

                var appointmentExists = await _context.Appointments
                    .FirstOrDefaultAsync(a => a.TimeSlotId == appointment.TimeSlotId);

                if (appointmentExists != null)
                {
                    response.Success = false;
                    response.Message = "The selected time slot is already booked.";
                    return response;
                }

                await _context.Appointments.AddAsync(appointment);
                await _context.SaveChangesAsync();

                response.Data = true;
                response.Message = "Appointment booked successfully";

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;

                return response;
            }
        }
    }
}
