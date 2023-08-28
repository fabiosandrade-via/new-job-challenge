using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using new_job_challenge.carrefour.application.Common.Models.DTOs;
using new_job_challenge.carrefour.application.Interfaces;
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
        private readonly IMapper _mapper;
        private readonly ILogger<AccountMovementController> _logger;
        private readonly IAccountMovementService _accountMovementService;
        private readonly IAccountMovementPostgresRepository _accountMovementPostgresRepository;
        private readonly IAccountMovementRedisRepository _accountMovementRedisRepository;
        private readonly IDistributedCache _distributedCache;

        public AccountMovementController(ILogger<AccountMovementController> logger, 
                                         IMapper mapper, 
                                         IAccountMovementService accountMovementService,
                                         IAccountMovementPostgresRepository accountMovementPostgresRepository,
                                         IAccountMovementRedisRepository accountMovementRedisRepository,
                                         IDistributedCache distributedCache)
        {
            _logger = logger;
            _mapper = mapper;
            _accountMovementService = accountMovementService;
            _accountMovementPostgresRepository = accountMovementPostgresRepository;
            _accountMovementRedisRepository = accountMovementRedisRepository;
            _distributedCache = distributedCache;
          }

        [Authorize]
        [HttpGet(Name = "GetAccountMoviment")]
        [ProducesResponseType(typeof(IEnumerable<AccountDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public IActionResult Get()
        {
            try
            {
                _logger.LogInformation("Listagem de movimentações de conta bancária recebida.");
                var listAccount = _accountMovementRedisRepository.Get(_distributedCache).Result;
                return Ok(listAccount);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost(Name = "SetAccountMoviment")]
        [ProducesResponseType(typeof(AccountDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public IActionResult AccountMoviment([FromBody] AccountDTO accountDTO)
        {
            try
            {
                if ((accountDTO.TransactionType != TransactionType.Debit) && (accountDTO.TransactionType != TransactionType.Credit))
                {
                    string message = "Operação de transação inválida. Selecione 1 = Débito ou 2 = Crédito";
                    return Ok(message);
                }

                _logger.LogInformation("Salvar movimentações de conta bancária.");

                var accountEntity = _mapper.Map<AccountEntity>(accountDTO);
                _accountMovementService.SaveAccountMovement(accountEntity, _accountMovementPostgresRepository, _accountMovementRedisRepository, _distributedCache);

                return Ok("Processamento em andamento.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}