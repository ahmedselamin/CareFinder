namespace CareFinder.Server.Models
{
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public byte[] PasswordHash { get; set; } = new byte[32];
        [Required]
        public byte[] PasswordSalt { get; set; } = new byte[32];
        [Required]
        public string Specialty { get; set; } = string.Empty;
        [Required]
        public string PracticeName { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public List<string> Qualifications { get; set; } = new List<string>();
        [Required]
        public string About { get; set; } = string.Empty;
        public List<AvailabilitySlot> AvailabilitySlots { get; set; } = new List<AvailabilitySlot>();
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
