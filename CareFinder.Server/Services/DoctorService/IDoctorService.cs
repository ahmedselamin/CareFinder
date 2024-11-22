namespace CareFinder.Server.Services.DoctorService
{
    public interface IDoctorService
    {
        Task<ServiceResponse<List<Doctor>>> FetchDoctor();
        Task<ServiceResponse<Doctor>> FetchDoctor(int doctorId);
        Task<ServiceResponse<List<DoctorSearchDTO>>> SearchDoctors(string searchText);
        Task<ServiceResponse<List<AvailabilitySlot>>> GetAvailabilitySlots(int doctorId);
        Task<ServiceResponse<List<AvailabilitySlot>>> AddAvailabilitySlots(int doctorId, SlotDTO slot);
    }
}
