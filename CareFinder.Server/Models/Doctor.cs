namespace CareFinder.Server.Models
{
    public class Doctor : User
    {
        [Required]
        public string Specialty { get; set; } = string.Empty;
        [Required]
        public string PracticeName { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public required List<string> Qualifications { get; set; } = new List<string>();
        [Required]
        public string About { get; set; } = string.Empty;
    }
}
