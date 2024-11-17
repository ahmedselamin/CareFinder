namespace CareFinder.Server.Models
{
    public class Admin : User
    {
        public string Role { get; set; } = "Admin";
    }
}
