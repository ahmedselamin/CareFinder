namespace CareFinder.Server.Models
{
    public class AvailabilitySlot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int AppointmentId { get; set; }
        public required string Day { get; set; }
        public TimeOnly StartsAt { get; set; }
        public TimeOnly EndsAt { get; set; }
        public int BreakInterval { get; set; }
    }
}
