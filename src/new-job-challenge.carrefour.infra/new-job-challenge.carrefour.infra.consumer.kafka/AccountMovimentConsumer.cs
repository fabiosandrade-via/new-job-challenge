using Microsoft.Extensions.Caching.Distributed;
using new_job_challenge.carrefour.domain.Entities;
using new_job_challenge.carrefour.domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace new_job_challenge.carrefour.infra.consumer.kafka
{
    public class AccountMovimentConsumer : IAccountMovimentConsumer
    {
        public static IAccountMovementPostgresRepository? IAccountMovementPostgresRepository { get; private set; }
        public static IAccountMovementRedisRepository? IAccountMovementRedisRepository { get; private set; }
        public static IDistributedCache? IDistributedCache { get; private set; }

        public async void SetInfraDB(IAccountMovementPostgresRepository accountMovementPostgresRepository,
                                     IAccountMovementRedisRepository accountMovementRedisRepository,
                                     IDistributedCache distributedCache)
        {
            IAccountMovementPostgresRepository = accountMovementPostgresRepository;
            IAccountMovementRedisRepository = accountMovementRedisRepository;
            IDistributedCache = distributedCache;
   
            await Task.CompletedTask;
        }
    }
}
