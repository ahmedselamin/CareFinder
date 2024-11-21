namespace CareFinder.Server.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public required DbSet<Doctor> Doctors { get; set; }
        public required DbSet<AvailabilitySlot> AvailabilitySlots { get; set; }
        public required DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.AvailabilitySlots)
                .WithOne()
                .HasForeignKey(a => a.DoctorId);
        }
    }
}
