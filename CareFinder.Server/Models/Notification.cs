namespace CareFinder.Server.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public required string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
