namespace CareFinder.Server.DTOs
{
    public class SlotDTO
    {
        public int DoctorId { get; set; }
        public required string Day { get; set; }
        public TimeOnly StartsAt { get; set; }
        public TimeOnly EndsAt { get; set; }
    }
}
