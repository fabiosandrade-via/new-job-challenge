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
  
[![new-job-challenge Screen Shot][repository-screenshot]](https://github.com/fabiosandrade-via/new-job-challenge/blob/master/img/AccountApplication.png)
 
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
* MongoDB
* Redis

<p align="right">(<a href="#readme-top">volta ao topo</a>)</p>
  
<!-- GETTING STARTED -->
# ➡️ Começando  
## 🚧 pré-requisitos

Este é um exemplo de como listar as bibliotecas utilizadas no software e como instalá-las.
* MongoDB (new_job_challenge.carrefour.infrastructure.repository)
  ```sh
  dotnet add package Newtonsoft --version 13.0.3
  ```
* Cirtuitbreak (new_job_challenge.carrefour.application)
  ```sh
  dotnet add package Polly --version 7.2.3
  ```
* Domínio (new_job_challenge.carrefour.domain)
  ```sh
  dotnet add package Newtonsoft --version 13.0.3
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
 
<p align="right">(<a href="#readme-top">volta ao topo</a>)</p>
 
<!-- DOCUMENTAÇÃO -->
# 💻 Documentação


Seguindo a estrutura de construção no padrão .NET Core 6 as instâncias foram implementadas na classe Program.cs por meio do componente builder responsável pela criação dos objetos. O Design Pattern de Injeção de Depedências foi o utilizado para comunicação entre as classes de Controle, Domínio e Serviço.
Sobre as divisões e responsabilidades por camadas:
 - Serviço: Responsável pela comunicação da aplicação com o Consul por meio da biblioteca Consul na versão 1.6.10.9. A conexão realizada ao Consul trabalhando como SaaS é feita por meio de Blocking Queries. Trata-se do gerenciamento da requisição, retornando o valor da chave quando o parâmetro index for diferente, ou quando o  tempo de espera (wait) atingir o tempo configurado na parametrização. No nosso exemplo são 40 segundos a cada chamada realizada.
  ```c#
//TODO
  ```
 - Domínio: Responsável por compartilhar componentes comuns entre os projetos como por exemplo interfaces e classes de modelo refentes a entradas e saídas formatadas por json.
  ```c#
//TODO
  ``` 
 - Controle: Responsável pela disponibilização dos endpoints para o consumo externo (API) na arquitetura REST na qual os contratos são visíveis através do Swagger.
  ```c#
 //TODO
  ``` 
 - Infraestrutura: Responsável pelo gerenciamento do consumo do serviço quando houver indisponibilidade do mesmo, retornando um status válido mediante a resposta de sucesso ou falha sem que ocorra a trava/quebra do retorno por meio da biblioteca Polly 7.2.3
  ```c#
//TODO
  ```


## Instruções de uso: 
<br />

  
[![new-job-challenge Screen Shot][repository-screenshot-swagg-1]](../new-job-challenge/img/1-Swagger.PNG)

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

  
[![new-job-challenge Screen Shot][repository-screenshot-swagg-2]](../new-job-challenge/img/2.0-Swagger-JsonStructurePathRequest.PNG)
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

[repository-screenshot]: img/AccountApplication.png
[repository-screenshot-swagg-1]: img/1-Swagger.PNG
[repository-screenshot-swagg-2]: img/2-Swagger-Set-Token.PNG
[repository-screenshot-swagg-2-1]: img/2.1-Swagger-JsonStructurePathResponse.PNG
[repository-screenshot-swagg-3]: img/3.0-Swagger-KeyValuePathRequest.PNG
[repository-screenshot-swagg-3-1]: img/3.1-Swagger-KeyValuePathResponse.PNG
