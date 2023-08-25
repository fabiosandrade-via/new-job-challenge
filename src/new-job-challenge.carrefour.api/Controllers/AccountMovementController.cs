using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using new_job_challenge.carrefour.application.Common.Models.DTOs;
using new_job_challenge.carrefour.domain.Common.Enums;
using new_job_challenge.carrefour.domain.Entities;
using new_job_challenge.carrefour.domain.Interfaces;
using System.Net;

namespace new_job_challenge.carrefour.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountMovementController : ControllerBase
    {
        IMapper _mapper;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<AccountMovementController> _logger;
        private readonly IAccountMovementService _accountMovementService;

        public AccountMovementController(ILogger<AccountMovementController> logger, IMapper mapper, 
                                         IAccountMovementService accountMovementService)
        {
            _logger = logger;
            _mapper = mapper;
            _accountMovementService = accountMovementService;
        }

        [Authorize]
        [HttpGet(Name = "GetAccountMoviment")]
        [ProducesResponseType(typeof(IEnumerable<AccountMoviment>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public IEnumerable<AccountMoviment> Get()
        {
            _logger.LogInformation("Listagem de movimenta��es de conta banc�ria recebida.");

            return Enumerable.Range(1, 5).Select(index => new AccountMoviment
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [Authorize]
        [HttpPost(Name = "SetAccountMoviment")]
        [ProducesResponseType(typeof(IEnumerable<AccountMoviment>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public IActionResult AccountMoviment([FromBody] AccountDTO accountDTO)
        {
            if ((accountDTO.TransactionType != TransactionType.Debit) && (accountDTO.TransactionType != TransactionType.Credit))
            {
                string message = "Opera��o de transa��o inv�lida. Selecione 1 = D�bito ou 2 = Cr�dito";
                return Ok(message);
            }

            _logger.LogInformation("Salvar movimenta��es de conta banc�ria.");

            var accountEntity = _mapper.Map<AccountEntity>(accountDTO);
            _accountMovementService.SaveAccountMovement(accountEntity);

            return Ok("Processamento em andamento.");
        }
    }
}