using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace new_job_challenge.carrefour.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountMovement : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<AccountMovement> _logger;

        public AccountMovement(ILogger<AccountMovement> logger)
        {
            _logger = logger;
        }

        [Authorize]
        [HttpGet(Name = "GetAccountMoviment")]
        [ProducesResponseType(typeof(IEnumerable<AccountMoviment>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public IEnumerable<AccountMoviment> Get()
        {
            _logger.LogInformation("Listagem de movimentações de conta bancária recebida.");

            return Enumerable.Range(1, 5).Select(index => new AccountMoviment
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}