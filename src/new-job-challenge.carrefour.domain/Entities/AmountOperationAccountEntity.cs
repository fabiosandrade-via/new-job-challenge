using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace new_job_challenge.carrefour.domain.Entities
{
    public class AmountOperationAccountEntity
    {
        public AccountEntity Account { get; set; }
        public DateTime OperationDate { get; set; }
        public decimal Amount { get; set; }

        public AmountOperationAccountEntity(AccountEntity account)
        {
            Account = account;
        }
    }
}
