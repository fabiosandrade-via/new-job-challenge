using Microsoft.Extensions.Caching.Distributed;
using new_job_challenge.carrefour.domain.Entities;

namespace new_job_challenge.carrefour.domain.Interfaces
{
    public interface IAccountMovementRedisRepository
    {
        Task<string> Get(IDistributedCache distributedCache);
        void Save(AmountOperationAccountEntity operationAccount, IDistributedCache distributedCache);
    }
}