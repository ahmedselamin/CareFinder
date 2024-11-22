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

        public async Task<ServiceResponse<List<AvailabilitySlot>>> AddAvailabilitySlot(int doctorId, SlotDTO slot)
        {
            var response = new ServiceResponse<List<AvailabilitySlot>>();
            var slots = new List<AvailabilitySlot>();

            try
            {
                if (slot.StartsAt >= slot.EndsAt)
                {
                    response.Success = false;
                    response.Message = "Start time should be before end time";

                    return response;
                };

                if (slot.BreakInterval >= 0)
                {
                    response.Success = false;
                    response.Message = "You must add break interval";

                    return response;
                };

                var currentStart = slot.StartsAt;  //staring point
                while (currentStart <= slot.EndsAt)
                {
                    var currentEnd = slot.StartsAt.AddMinutes(slot.BreakInterval); //endpoint for slot

                    //stop if the current slot exceeds the end time
                    if (currentEnd > slot.EndsAt)
                        break;

                    //create new slot
                    var newSlot = new AvailabilitySlot
                    {
                        DoctorId = doctorId,
                        Day = slot.Day,
                        StartsAt = currentStart,
                        EndsAt = currentEnd,
                        BreakInterval = slot.BreakInterval,
                    };

                    slots.Add(newSlot);

                    //move to next slot
                    currentStart = currentEnd;
                }

                await _context.AvailabilitySlots.AddRangeAsync(slots);
                await _context.SaveChangesAsync();

                response.Data = slots;
                response.Message = "New slot added";

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;

                return response;
            }
        }

        public async Task<ServiceResponse<List<AvailabilitySlot>>> GetAvailabilitySlots(int doctorId)
        {
            var response = new ServiceResponse<List<AvailabilitySlot>>();

            try
            {
                var slots = await _context.AvailabilitySlots
                    .Where(a => a.Id == doctorId)
                    .ToListAsync();

                response.Data = slots;

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;

                return response;
            }
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
