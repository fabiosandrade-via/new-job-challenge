namespace new_job_challenge.carrefour.domain.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken();
        Task<bool> Login(string userName, string password);
    }
}