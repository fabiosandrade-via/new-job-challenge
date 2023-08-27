using Microsoft.Extensions.Caching.Distributed;
using new_job_challenge.carrefour.application.Interfaces;
using new_job_challenge.carrefour.domain.Entities;
using new_job_challenge.carrefour.domain.Interfaces;
using new_job_challenge.carrefour.infrastructure.db.postgres.Repository;
using new_job_challenge.carrefour.infrastructure.security.Services.AccountMoviment.Calc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace new_job_challenge.carrefour.infrastructure.security.Services.AccountMoviment
{
    public class AccountMovementService : IAccountMovementService
    {    
        public void SaveAccountMovement(AccountEntity accountEntity, 
                                        IAccountMovementPostgresRepository accountMovementPostgresRepository,
                                        IAccountMovementRedisRepository accountMovementRedisRepository,
                                        IDistributedCache distributedCache)
        {
            AmountOperationAccountEntity amountOperationAccountEntity = new CalcAmountOperationAccount(accountEntity).GetAmountOperationAccount();
            //accountMovementRedisRepository.Save(amountOperationAccountEntity, distributedCache);
            //accountMovementPostgresRepository.AmountOperationAccountEntities.Add(amountOperationAccountEntity);
        }
    }
}
