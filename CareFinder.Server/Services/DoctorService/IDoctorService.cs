namespace CareFinder.Server.Services.DoctorService
{
    public interface IDoctorService
    {
        Task<ServiceResponse<List<DoctorSearchDTO>>> SearchDoctors(string searchText);
        Task<ServiceResponse<Doctor>> GetDoctor(int doctorId);
        Task<ServiceResponse<List<AvailabilitySlot>>> AddAvailabilitySlot(int doctorId, SlotDTO slot);
        Task<ServiceResponse<List<AvailabilitySlot>>> GetAvailabilitySlots(int doctorId);
    }
}
