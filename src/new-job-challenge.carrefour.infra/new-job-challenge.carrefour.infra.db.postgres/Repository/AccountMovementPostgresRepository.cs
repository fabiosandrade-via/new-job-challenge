using Microsoft.EntityFrameworkCore;
using new_job_challenge.carrefour.domain.Entities;
using new_job_challenge.carrefour.domain.Interfaces;

namespace new_job_challenge.carrefour.infra.db.postgres.Repository
{
    public class AccountMovementPostgresRepository : DbContext, IAccountMovementPostgresRepository
    {
        public DbSet<AmountOperationAccountEntity> AmountOperationAccountEntities { get; set; }

        public AccountMovementPostgresRepository(DbContextOptions<AccountMovementPostgresRepository> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AmountOperationAccountEntity>()
                .Property(x => x.Account)
                .HasColumnType("jsonb");
        }
    }
}