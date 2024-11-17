namespace CareFinder.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(Doctor doctor, string password);
        Task<ServiceResponse<string>> Login(string email, string password);
    }
}
