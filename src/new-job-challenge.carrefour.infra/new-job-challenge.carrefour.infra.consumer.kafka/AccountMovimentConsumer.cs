using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace new_job_challenge.carrefour.infra.consumer.kafka
{
    public class AccountMovimentConsumer : IAccountMovimentConsumer
    {
        public async Task<string> GetTest()
        {
            string retorno = "Fabio";
            return Task.FromResult(retorno).Result;
        }
    }
}
