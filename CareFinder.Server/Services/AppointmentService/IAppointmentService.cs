namespace CareFinder.Server.Services.AppointmentService
{
    public interface IAppointmentService
    {
        Task<ServiceResponse<bool>> CreateAppointment(Appointment appointment);
    }
}
