using new_job_challenge.carrefour.application.Common.Models.DTOs;
using new_job_challenge.carrefour.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace new_job_challenge.carrefour.infrastructure.security.Services.AccountMoviment
{
    public class AmountOperationAccountService
    {
        private AmountOperationAccountEntity _amountOperationAccount;

        private decimal amount = 1000;

        public AmountOperationAccountService(AccountEntity accountEntity)
        {
            _amountOperationAccount = new AmountOperationAccountEntity(accountEntity);
        }
        private void AddCredit()
        {
            amount += _amountOperationAccount.Account.OperationValue;
        }
        private void AddDebit()
        {
            amount += _amountOperationAccount.Account.OperationValue;
        }
        public void SaveAmountOperationAccount()
        {
            _amountOperationAccount.Amount = amount;
            _amountOperationAccount.OperationDate = DateTime.Now;

            if (_amountOperationAccount.Account.TransactionType == domain.Common.Enums.TransactionType.Credit)
                AddCredit();
            else
                AddDebit();

            // TODO salvar na base de dados chamar repository
        }
    }
}
