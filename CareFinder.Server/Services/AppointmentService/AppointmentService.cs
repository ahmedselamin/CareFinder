
namespace CareFinder.Server.Services.AppointmentService
{
    public class AppointmentService : IAppointmentService
    {
        private readonly DataContext _context;

        public INotificationService _notificationService { get; }

        public AppointmentService(DataContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        public async Task<ServiceResponse<List<Appointment>>> FetchAllAppointments(int doctorId)
        {
            var response = new ServiceResponse<List<Appointment>>();

            try
            {
                var appointments = await _context.Appointments
                    .Where(a => a.DoctorId == doctorId)
                    .ToListAsync();

                response.Data = appointments;

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;

                return response;
            }
        }
        public async Task<ServiceResponse<Appointment>> FetchAppointment(int doctorId, int appointmentId)
        {
            var response = new ServiceResponse<Appointment>();

            try
            {
                var appointment = await _context.Appointments
                    .FirstOrDefaultAsync(a => a.Id == appointmentId && a.DoctorId == doctorId);

                if (appointment == null)
                {
                    response.Success = false;
                    response.Message = "Not found";

                    return response;
                }

                response.Data = appointment;

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;

                return response;
            }
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

                var message = $"{doctor.FullName}, you have a new appointment scheduled.";
                await _notificationService.SendNofication(doctor.Id, message);

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
