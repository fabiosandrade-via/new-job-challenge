using new_job_challenge.carrefour.domain.Entities;

namespace new_job_challenge.carrefour.domain.Interfaces
{
    public interface IAccountMovementService
    {
        void SaveAccountMovement(AccountEntity accountEntity);
    }
}