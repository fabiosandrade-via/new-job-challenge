using new_job_challenge.carrefour.application.Common.Models.DTOs;
using new_job_challenge.carrefour.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace new_job_challenge.carrefour.infra.security.Services.AccountMoviment.Calc
{
    public class CalcAmountOperationAccount
    {
        private AmountOperationAccountEntity _amountOperationAccount;

        private decimal amount = 1000;

        public CalcAmountOperationAccount(AccountEntity accountEntity)
        {
            _amountOperationAccount = new AmountOperationAccountEntity(accountEntity);
        }
        private void AddCredit()
        {
            amount += _amountOperationAccount.Account.OperationValue;
        }
        private void AddDebit()
        {
            amount -= _amountOperationAccount.Account.OperationValue;
        }
        public AmountOperationAccountEntity GetAmountOperationAccount()
        {
            if (_amountOperationAccount.Account.TransactionType == domain.Common.Enums.TransactionType.Credit)
                AddCredit();
            else
                AddDebit();

            _amountOperationAccount.OperationDate = DateTime.Now;
            _amountOperationAccount.Amount = amount;

            return _amountOperationAccount;
        }
    }
}
