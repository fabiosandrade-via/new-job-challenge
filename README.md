# Trabalho construído para avaliação técnica do Banco Carrefour
<!-- Permite  a funcionalidade de voltar ao topo -->
<a name="readme-top"></a>
 
<!-- PROJECT SHIELDS -->
<!--
*** Estou usando markdown "reference style ou estilo de referencia" para melhor leitura dos links.
*** Links de Referencia sao fechados com brackets [ ] ao inves de parenteses ( ).
*** Veja no final desse documento para declarar as variaveis de referencia
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
 

<!-- LOGO -->
<br />
<div align="center">
  
[![new-job-challenge Screen Shot][repository-screenshot]](https://github.com/fabiosandrade-via/new-job-challenge/blob/main/docs/img/AccountApplication.png)
 
<h2 align="center">Demo Aplicação - Banca avaliadora</h2>
   
  <p align="center">
  A aplicação tem como objetivo demonstrar de forma simplificada através de um projeto público como realizar movimentações de saldo de contas bancárias de clientes      
  </p>
</div>
<br />
 
<!-- ÍNDICE DOS CONTEUDOS -->
<summary>👉 Índice</summary>
  <ol>
    <li>
      <a href="#📄-sobre-o-projeto">📄 Sobre o Projeto</a>
    </li>
    <li>
      <a href="#➡️-começando">➡️ Começando</a>
      <ul>
        <li><a href="#🚧-pré-requisitos">🚧 Pré-requisito</a></li>
        <li><a href="#⚙️-instalação">⚙️ Instalação</a></li>
      </ul>
    </li>
    <li><a href="#💻-documentação">💻 Documentação</a></li>
    <li><a href="#🧪-testes">🧪 Testes</a></li>
    <li><a href="#👷-candidato">👷 Candidato</a></li>
  </ol>

</br>
</br>
 
<!-- SOBRE O PROJETO -->
# 📝 Sobre o Projeto
O projeto demonstra de forma simplificada como são realizadas movimentações de contas bancárias. A partir de imagens docker referentes a tecnologias de de desenvolvimento de software, mensageria, logs e bancos de dados, apresentar o PIPELINE contendo requisitos funcionais e não funcionais da aplicação.

## Principais tecnologias e padrões utilizados:
* Clean Architecture
* .NET Core 6
* EDA
* Kafka
* Serilog
* Swagger
* Docker
* Docker Compose
* Postgres
* Redis

<p align="right">(<a href="#readme-top">volta ao topo</a>)</p>
  
<!-- GETTING STARTED -->
# ➡️ Começando  
## 🚧 pré-requisitos

Este é um exemplo de como listar as bibliotecas utilizadas no software e como instalá-las.
* Postgres
  ```sh
  dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 7.0.4
  ```
* Redis
  ```sh
  dotnet add package Microsoft.Extensions.Caching.Abstractions --version 7.0.0
  ```
* Kafka
  ```sh
  dotnet add package Confluent.Kafka --version 2.2.0
  ```
   
## ⚙️ instalação
 
1. Procure na estrutura fabiosandrade-via por new-job-challenge [https://github.com/fabiosandrade-via/new-job-challenge](https://github.com/fabiosandrade-via/new-job-challenge)
2. Clone o repositório
   ```sh
   git clone fabiosandrade-via/new-job-challenge
   ```
3. Para visualizar o projeto você pode utilizar a IDE do Visual Studio ou VS Code.
   ```sh
   https://visualstudio.microsoft.com/pt-br/
   https://code.visualstudio.com/
   ```
4. Confira as parametrizações em `appsettings.json` para geração de tokens
   ```js
   "Jwt": {
    "Key": "dfusa7f9090dfsiaisfdasfiuasjasdfa90cvzxxzcvasf998dfspd",
    "Issuer": "TesteIssuer",
    "Audience": "TesteAudience"
   }
   ```
 5. Executando a aplicação
   ```sh
    docker-compose up -d
   ```
<p align="right">(<a href="#readme-top">volta ao topo</a>)</p>
 
<!-- DOCUMENTAÇÃO -->
# 💻 Documentação


Seguindo a estrutura de construção no padrão .NET Core 6 as instâncias foram implementadas na classe Program.cs por meio do componente builder responsável pela criação dos objetos. O Design Pattern de Injeção de Depedências foi o utilizado para comunicação entre as classes de Controle, Domínio e Serviço.
Sobre as divisões e responsabilidades por camadas:
 - Serviço: Responsável pela comunicação da aplicação com o Kafka por meio da biblioteca Confluent.Kafka na versão 2.2.0. A conexão realizada ao Kafka trabalhando como SaaS (Container) é feita por meio da network do docker compose. A partir de um produtor as movimentações de conta geradas como evento são inseridas em um tópico para armazenamento que posteriormente serão consumidas por um consumer.
  ```c#
    public class AccountMovementService : IAccountMovementService
    {    
        public async void SaveAccountMovement(AccountEntity accountEntity, 
                                        IAccountMovementPostgresRepository accountMovementPostgresRepository,
                                        IAccountMovementRedisRepository accountMovementRedisRepository,
                                        IDistributedCache distributedCache)
        {
            AmountOperationAccountEntity amountOperationAccountEntity = new CalcAmountOperationAccount(accountEntity).GetAmountOperationAccount();
            ProducerBrokerKafka.Send<AmountOperationAccountEntity>(amountOperationAccountEntity);

            await Task.CompletedTask;
        }
    }
  ```
  Kafka Producer
```c#
       public static void Send<T>(T entity) where T: class
        {
            string topic = "testtopic";

            var config = new ClientConfig()
            {
                BootstrapServers = "127.0.0.1:9092",

            };

            using (var producer = new ProducerBuilder<string, string>(config).Build())
            {
                var key = "movimento-conta";
                var val = JObject.FromObject(new { entity }).ToString(Formatting.None);

                Console.WriteLine($"Produzindo mensagem: {key} {val}");

                producer.Produce(topic, new Message<string, string> { Key = key, Value = val },
                    (deliveryReport) =>
                    {
                        if (deliveryReport.Error.Code != ErrorCode.NoError)
                        {
                            Console.WriteLine($"Falha para enviar mensagem: {deliveryReport.Error.Reason}");
                        }
                        else
                        {
                            Console.WriteLine($"Mensagem produzida para: {deliveryReport.TopicPartitionOffset}");
                        }
                    });

                producer.Flush(TimeSpan.FromSeconds(10));
            }
        }
    }
 
  ``` 
Kafka Consumer: O consumo das mensagens são armazenadas nas bases Postgres e Redis.
  ```c#
            using (var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build())
            {
                consumer.Subscribe(topic);

                try
                {
                    while (true)
                    {
                        var cr = consumer.Consume(cts.Token);
                        AmountOperationAccountEntity amountOperationAccountEntity = (AmountOperationAccountEntity)JsonConvert.DeserializeObject(cr.Message.Value);
                        
                        AccountMovimentConsumer.IAccountMovementRedisRepository.Save(amountOperationAccountEntity, AccountMovimentConsumer.IDistributedCache);
                        AccountMovimentConsumer.IAccountMovementPostgresRepository.AmountOperationAccountEntities.Add(amountOperationAccountEntity);

                        Console.WriteLine($"Consumo de registro com a chave {cr.Message.Key} e valor {cr.Message.Value}");
                    }
                }
                catch (OperationCanceledException)
                {
                    // Ctrl-C was pressed.
                }
                finally
                {
                    consumer.Close();
                }
            }

  ``` 
 
 - Domínio: Responsável por compartilhar componentes comuns entre os projetos como por exemplo interfaces e classes de modelo refentes a entradas e saídas formatadas por json.
  ```c#
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
  ``` 
 - Controle: Responsável pela disponibilização dos endpoints para o consumo externo (API) na arquitetura REST na qual os contratos são visíveis através do Swagger.
  ```c#

        [Authorize]
        [HttpPost(Name = "SetAccountMoviment")]
        [ProducesResponseType(typeof(AccountDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public IActionResult AccountMoviment([FromBody] AccountDTO accountDTO)
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
  ``` 
 - Infraestrutura: Responsável pelo gerenciamento de armazenamento dos dados.
 Postgres:
  ```c#
    public class AccountMovementPostgresRepository : DbContext, IAccountMovementPostgresRepository
    {
        public DbSet<AmountOperationAccountEntity> AmountOperationAccountEntities { get; set; }

        public AccountMovementPostgresRepository(DbContextOptions<AccountMovementPostgresRepository> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AmountOperationAccountEntity>()
                .Property(x => x.Account)
                .HasColumnType("jsonb");
        }
    }
  ```
Redis:
  ```c#
    public class AccountMovementRedisRepository : IAccountMovementRedisRepository
    {
        const string _cacheKey = "new-challenge";

        public async Task<string> Get(IDistributedCache distributedCache)
        {
            return await distributedCache.GetStringAsync(_cacheKey);
        }
        public async void Save(AmountOperationAccountEntity operationAccount, IDistributedCache distributedCache)
        {
            var _options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(24));
            var _json = JsonConvert.SerializeObject(operationAccount, Formatting.Indented);
            await distributedCache.SetStringAsync(_cacheKey, _json, _options);
        }
    }
  ```
Crosscutting:
  ```c#
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            var teste = services.AddDbContext<AccountMovementPostgresRepository>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("RedisConnection");
            });

            services.AddScoped<IAccountMovementPostgresRepository, AccountMovementPostgresRepository>();
            services.AddScoped<IAccountMovementRedisRepository, AccountMovementRedisRepository>();
            services.AddDbContext<AccountMovementPostgresRepository>();
        }
    }
  ```
## Instruções de uso: 
<br />

  
[![new-job-challenge Screen Shot][repository-screenshot-swagg-1]](../docs/img/1-Swagger.png)

Para realizar o consumo da API é necessária geraração de um token com prazo de expiração de 60 minutos.
O método Token (POST) deverá ser preenchido com a seguinte requisição (Request body):
   ```js
    {
        "userName": "avalia",
        "password": "teste"
    }
   ```

- Na sequência, o token gerado deverá ser imputado no campo value: Bearer + o token gerado e logo após, pressione o botão [Authorize].
<br />

  
[![new-job-challenge Screen Shot][repository-screenshot-swagg-2]](../docs/img/2-Swagger-Set-Token.png.PNG)
[![new-job-challenge Screen Shot][repository-screenshot-swagg-2-1]](../new-job-challenge/img/2.1-Swagger-JsonStructurePathResponse.PNG)

- Realizados esses procedimentos, será possível realizar as movimentações bancárias na conta do cliente.
 
[![new-job-challenge Screen Shot][repository-screenshot-swagg-3]](../new-job-challenge/img/3.0-Swagger-KeyValuePathRequest.PNG)
[![new-job-challenge Screen Shot][repository-screenshot-swagg-3-1]](../new-job-challenge/img/3.1-Swagger-KeyValuePathResponse.PNG)
 
<p align="right">(<a href="#readme-top">volta ao topo</a>)</p>
 
<!-- TESTES -->
# 🧪 Testes
 
Os testes foram criados utilizando as bibliotecas xunit na versão 2.4.3 e Moq na versão 4.18.4
Para a execução através de CLI é necessário estar na pasta do projeto de testes.
```sh
dotnet test
```
Atualmente, a cobertura de código está em cerca de 80%, o que significa que a maioria das funcionalidades está coberta por testes automatizados. Você pode verificar a cobertura de código executando o seguinte comando:
 
```sh
dotnet run coverage
```
Isso irá gerar um relatório em HTML que pode ser aberto em um navegador para ver detalhes sobre a cobertura de código em diferentes arquivos e funções.
 
Nós incentivamos todos os desenvolvedores a escreverem testes para novas funcionalidades e manterem a cobertura de código em um nível alto para garantir a qualidade do código.
 
<p align="right">(<a href="#readme-top">volta ao topo</a>)</p>
  
<!-- RESPONSAVEL -->
# 👷 Candidato
 
 Linkedin: [Fábio Santos de Andrade](https://www.linkedin.com/in/fabio-santos-de-andrade-78334b22/)

 E-Mail: fabiosantosdeandrade.bh@gmail.com
 

<p align="right">(<a href="#readme-top">volta ao topo</a>)</p>



<!-- MARKDOWN LINKS & IMAGES -->

[repository-screenshot]: docs/img/AccountApplication.png
[repository-screenshot-swagg-1]: docs/img/1-Swagger.png
[repository-screenshot-swagg-2]: docs/img/2-Swagger-Set-Token.png
[repository-screenshot-swagg-2-1]: docs/img/3-Swagger-Set-Token.png
[repository-screenshot-swagg-3]: docs/img/4-Swagger.png
[repository-screenshot-swagg-3-1]: docs/img/5-Swagger.png
