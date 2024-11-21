namespace CareFinder.Server.Services.NotificationService
{
    public class NotificationService : INotificationService
    {
        private readonly DataContext _context;

        public NotificationService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<List<Notification>>> FetchAllNotification(int doctorId)
        {
            var response = new ServiceResponse<List<Notification>>();

            try
            {
                var notifications = await _context.Notifications
                    .Where(n => n.DoctorId == doctorId)
                    .OrderByDescending(n => n.Timestamp)
                    .ToListAsync();

                response.Data = notifications;

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;

                return response;
            }
        }

        public async Task<ServiceResponse<bool>> SendNofication(int doctorId, string message)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                var notifcation = new Notification
                {
                    DoctorId = doctorId,
                    Message = message,
                    Timestamp = DateTime.Now,
                };

                await _context.Notifications.AddAsync(notifcation);
                await _context.SaveChangesAsync();

                response.Data = true;

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
