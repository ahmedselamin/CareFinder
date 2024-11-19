using Microsoft.Extensions.Caching.Memory;

namespace CareFinder.Server.Services.DoctorService
{
    public class DoctorService : IDoctorService
    {
        private readonly IMemoryCache _cache;
        private readonly DataContext _context;

        public DoctorService(IMemoryCache cache, DataContext context)
        {
            _cache = cache;
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
            var cacheKey = $"SearchDoctors_{searchText.ToLower()}";

            try
            {
                if (!_cache.TryGetValue(cacheKey, out List<DoctorSearchDTO>? cachedDoctors))
                {
                    cachedDoctors = await _context.Doctors
                    .Where(d => d.FullName.ToLower().Contains(searchText.ToLower())
                      || d.Specialty.ToLower().Contains(searchText.ToLower())
                      || d.City.ToLower().Contains(searchText.ToLower())
                    ).Select(d => new DoctorSearchDTO
                    {
                        Id = d.Id,
                        FullName = d.FullName,
                        Specialty = d.Specialty,
                        City = d.City,
                    }).ToListAsync();

                    //cache results
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, cachedDoctors, cacheEntryOptions);
                }

                response.Data = cachedDoctors;
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
