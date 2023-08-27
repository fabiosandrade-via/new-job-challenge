namespace new_job_challenge.carrefour.application.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken();
        Task<bool> Login(string userName, string password);
    }
}