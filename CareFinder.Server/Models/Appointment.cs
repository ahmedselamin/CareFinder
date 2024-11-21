namespace CareFinder.Server.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int TimeSlotId { get; set; }
        public required string PatientFullName { get; set; }
    }
}
