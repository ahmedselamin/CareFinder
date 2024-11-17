namespace CareFinder.Server.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public required DbSet<Doctor> Doctors { get; set; }
    }
}
