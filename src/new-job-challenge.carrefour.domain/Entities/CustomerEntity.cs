using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace new_job_challenge.carrefour.domain.Entities
{
    public class CustomerEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
