namespace CareFinder.Server.Services.NotificationService
{
    public interface INotificationService
    {
        Task<ServiceResponse<List<Notification>>> FetchAllNotification(int doctorId);
        Task<ServiceResponse<bool>> SendNofication(int doctorId, string message);
    }
}
