using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using new_job_challenge.carrefour.application.Common.Models.DTOs;
using new_job_challenge.carrefour.domain.Entities;
using new_job_challenge.carrefour.domain.Interfaces;
using new_job_challenge.carrefour.infrastructure.security.Services.AccountMoviment;
using new_job_challenge.carrefour.service.Services.Token;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
	var keyAssign = configuration["Jwt:Key"];

	if (string.IsNullOrEmpty(keyAssign))
	{
		keyAssign = Guid.NewGuid().ToString();
	}

	var key = Encoding.UTF8.GetBytes(keyAssign);

	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = configuration["Jwt:Issuer"],
		ValidAudience = configuration["Jwt:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(key)
	};
});

builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "Demo Aplicação",
            Version = "v1",
            Description = "Apresentação para avaliação do Banco Carrefour"
		}
    ));

builder.Services.AddSwaggerGen(c =>
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description =
			"JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
			"Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
			"Exemplo (informar sem as aspas): 'Bearer 12345abcdef'",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
	}
	));

builder.Services.AddSwaggerGen(c =>
	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			Array.Empty<string>()
		}
	}));

builder.Services.AddScoped<ITokenService>(x => new TokenService(configuration));
builder.Services.AddScoped<IAccountMovementService>(x => new AccountMovementService());

var configMapper = new MapperConfiguration(cfg =>
{
	cfg.CreateMap<AccountDTO, AccountEntity>();
	cfg.CreateMap<CustomerDTO, CustomerEntity>();
});

IMapper mapper = configMapper.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
