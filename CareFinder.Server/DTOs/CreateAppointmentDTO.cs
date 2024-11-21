namespace CareFinder.Server.DTOs
{
    public class CreateAppointmentDTO
    {
        public int DoctorId { get; set; }
        public int TimeSlotId { get; set; }
        public required string PatientsFullName { get; set; }
    }
}
