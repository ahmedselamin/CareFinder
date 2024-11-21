namespace CareFinder.Server.Services.AppointmentService
{
    public interface IAppointmentService
    {
        Task<ServiceResponse<List<Appointment>>> FetchAllAppointments(int doctorId);
        Task<ServiceResponse<bool>> CreateAppointment(Appointment appointment);
    }
}
