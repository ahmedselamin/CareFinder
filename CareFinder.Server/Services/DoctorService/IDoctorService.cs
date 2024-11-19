namespace CareFinder.Server.Services.DoctorService
{
    public interface IDoctorService
    {
        Task<ServiceResponse<List<DoctorSearchDTO>>> SearchDoctors(string searchText);
        Task<ServiceResponse<Doctor>> GetDoctor(int doctorId);
        Task<ServiceResponse<AvailabilitySlot>> AddAvailabilitySlot(int doctorId, AvailabilitySlot slot);
    }
}
