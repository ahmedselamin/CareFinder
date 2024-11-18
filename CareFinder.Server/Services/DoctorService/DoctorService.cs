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

        public async Task<ServiceResponse<List<Doctor>>> SearchDoctors(string searchText)
        {
            var response = new ServiceResponse<List<Doctor>>();

            try
            {
                var doctor = await _context.Doctors
                     .Where(d => d.FullName.ToLower().Contains(searchText.ToLower())
                       ||
                       d.Specialty.ToLower().Contains(searchText.ToLower())
                     )
                     .ToListAsync();


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
