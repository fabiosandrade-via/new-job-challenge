using new_job_challenge.carrefour.domain.Entities;
using new_job_challenge.carrefour.domain.Interfaces;
using new_job_challenge.carrefour.service.Services.AccountMoviment.Calc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace new_job_challenge.carrefour.infrastructure.security.Services.AccountMoviment
{
    public class AccountMovementService : IAccountMovementService
    {
 
        public void SaveAccountMovement(AccountEntity accountEntity)
        {
            // TODO salvar AccountMovement na base de dados chamar repository
            new CalcAmountOperationAccount(accountEntity).SaveAmountOperationAccount();
        }
    }
}
