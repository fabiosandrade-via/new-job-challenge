using new_job_challenge.carrefour.domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace new_job_challenge.carrefour.application.Common.Models.DTOs
{
    public class AccountDTO
    {
        public CustomerDTO? Customer { get; set; }
        public string? Agency { get; set; }
        public string? Number { get; set; }
        public TransactionType TransactionType { get; set; } 
        public decimal OperationValue { get; set; }
    }
}
