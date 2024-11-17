namespace CareFinder.Server.Models
{
    public class Doctor : User
    {
        public string Specialty { get; set; } = string.Empty;
        public string PracticeName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public List<string> Qualifications { get; set; } = new List<string>();
        public string About { get; set; } = string.Empty;
    }
}
