namespace CareFinder.Server.DTOs
{
    public class DoctorSearchDTO
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Specialty { get; set; }
        public required string City { get; set; }
    }
}
