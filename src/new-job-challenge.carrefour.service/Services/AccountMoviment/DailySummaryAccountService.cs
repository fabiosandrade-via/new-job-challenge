using new_job_challenge.carrefour.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace new_job_challenge.carrefour.infrastructure.security.Services.AccountMoviment
{
    public class DailySummaryAccountService
    {
        private AmountOperationAccountEntity _dailySummaryAccount;

        private decimal totalCredit;
        private decimal totalDebit;
        private decimal netAmount => totalCredit - totalDebit;

        public DailySummaryAccountService(AccountEntity account)
        {
            _dailySummaryAccount = new AmountOperationAccountEntity(account);
        }
        private void AddCredit()
        {
            totalDebit = 0;
        }
        private void AddDebit()
        {

        }
    }
}
