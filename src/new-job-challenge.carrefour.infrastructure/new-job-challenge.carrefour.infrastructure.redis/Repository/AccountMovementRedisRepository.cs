using Microsoft.Extensions.Caching.Distributed;
using new_job_challenge.carrefour.domain.Entities;
using new_job_challenge.carrefour.domain.Interfaces;
using Newtonsoft.Json;

namespace new_job_challenge.carrefour.infrastructure.redis.Repository
{
    public class AccountMovementRedisRepository : IAccountMovementRedisRepository
    {
        const string _cacheKey = "new-challenge";

        public async Task<string> Get(IDistributedCache distributedCache)
        {
            return await distributedCache.GetStringAsync(_cacheKey);
        }
        public async void Save(Task<IQueryable<AmountOperationAccountEntity>> listOperationAccount, IDistributedCache distributedCache)
        {
            var _options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(24));
            var _json = JsonConvert.SerializeObject(listOperationAccount.Result, Formatting.Indented);
            await distributedCache.SetStringAsync(_cacheKey, _json, _options);
        }
    }
}