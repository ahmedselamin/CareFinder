namespace CareFinder.Server.DTOs
{
    public class SlotDTO
    {
        public required string Day { get; set; }
        public TimeOnly StartsAt { get; set; }
        public TimeOnly EndsAt { get; set; }
        public int BreakInterval { get; set; }
    }
}
