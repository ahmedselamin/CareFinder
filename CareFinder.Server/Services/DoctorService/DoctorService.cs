namespace CareFinder.Server.Services.DoctorService
{
    public class DoctorService : IDoctorService
    {
        private readonly DataContext _context;

        public DoctorService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Doctor>> GetDoctor(int doctorId)
        {
            var response = new ServiceResponse<Doctor>();

            try
            {
                var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);

                if (doctor == null)
                {
                    response.Success = false;
                    response.Message = "Not found";

                    return response;
                }

                response.Data = doctor;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;

                return response;
            }
        }

        public async Task<ServiceResponse<List<DoctorSearchDTO>>> SearchDoctors(string searchText)
        {
            var response = new ServiceResponse<List<DoctorSearchDTO>>();

            try
            {
                var doctor = await _context.Doctors
                     .Where(d => d.FullName.ToLower().Contains(searchText.ToLower())
                       ||
                       d.Specialty.ToLower().Contains(searchText.ToLower())
                     ).Select(d => new DoctorSearchDTO
                     {
                         Id = d.Id,
                         FullName = d.FullName,
                         Specialty = d.Specialty,
                         City = d.City,
                     }).ToListAsync();


                response.Data = doctor;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;

                return response;
            }
        }
    }
}
