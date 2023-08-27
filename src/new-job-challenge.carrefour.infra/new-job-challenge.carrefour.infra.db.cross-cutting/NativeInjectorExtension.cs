using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using new_job_challenge.carrefour.domain.Interfaces;
using new_job_challenge.carrefour.infra.db.postgres.Repository;
using new_job_challenge.carrefour.infra.redis.Repository;

namespace new_job_challenge.carrefour.infra.db.cross_cutting
{
    public static class NativeInjectorExtension
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            var teste = services.AddDbContext<AccountMovementPostgresRepository>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("RedisConnection");
            });

            services.AddScoped<IAccountMovementPostgresRepository, AccountMovementPostgresRepository>();
            services.AddScoped<IAccountMovementRedisRepository, AccountMovementRedisRepository>();
            services.AddDbContext<AccountMovementPostgresRepository>();
        }
    }
}
