
using Microsoft.Extensions.Caching.Distributed;
using new_job_challenge.carrefour.domain.Entities;
using new_job_challenge.carrefour.domain.Interfaces;

namespace new_job_challenge.carrefour.infra.consumer.kafka
{
    public interface IAccountMovimentConsumer
    {
        void SetInfraDB(IAccountMovementPostgresRepository accountMovementPostgresRepository,
                        IAccountMovementRedisRepository accountMovementRedisRepository,
                        IDistributedCache distributedCache);
    }
}