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
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.Appointments)
                .WithOne()
                .HasForeignKey(a => a.DoctorId)
                .IsRequired();

            modelBuilder.Entity<Appointment>()
                .HasOne<AvailabilitySlot>()
                .WithOne()
                .HasForeignKey<Appointment>(a => a.TimeSlotId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
