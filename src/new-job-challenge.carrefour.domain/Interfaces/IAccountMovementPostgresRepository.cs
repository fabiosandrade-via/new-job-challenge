using Microsoft.EntityFrameworkCore;
using new_job_challenge.carrefour.domain.Entities;

namespace new_job_challenge.carrefour.domain.Interfaces
{
    public interface IAccountMovementPostgresRepository
    {
        DbSet<AmountOperationAccountEntity> AmountOperationAccountEntities { get; set; }
    }
}