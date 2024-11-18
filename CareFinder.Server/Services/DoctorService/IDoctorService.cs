namespace CareFinder.Server.Services.DoctorService
{
    public interface IDoctorService
    {
        Task<ServiceResponse<Doctor>> GetDoctor(int doctorId);
        Task<ServiceResponse<List<DoctorSearchDTO>>> SearchDoctors(string searchText);
    }
}
