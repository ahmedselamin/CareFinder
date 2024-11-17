namespace CareFinder.Server.DTOs
{
    public class RegisterDTO
    {
        public required string Email { get; set; }
        public required string FullName { get; set; }
        public required string Password { get; set; }
        public required string Specialty { get; set; }
        public required string PracticeName { get; set; }
        public required string City { get; set; }
        public required List<string> Qualifications { get; set; } = new List<string>();
        public required string About { get; set; }
    }
}
