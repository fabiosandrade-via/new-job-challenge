using Microsoft.Extensions.Caching.Distributed;
using new_job_challenge.carrefour.domain.Entities;
using new_job_challenge.carrefour.domain.Interfaces;

namespace new_job_challenge.carrefour.application.Interfaces
{
    public interface IAccountMovementService
    {
        void SaveAccountMovement(AccountEntity accountEntity, 
                                 IAccountMovementPostgresRepository accountMovementPostgresRepository, 
                                 IAccountMovementRedisRepository accountMovementRedisRepository,
                                 IDistributedCache distributedCache);
    }
}