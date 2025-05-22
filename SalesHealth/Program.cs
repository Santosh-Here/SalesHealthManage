using AutoMapper;
using SalesHealth.Adapters;
using SalesHealth.Adapters.Interfaces;
using SalesHealth.Cores;
using SalesHealth.Models.Dtos;
using SalesHealth.Repositories;
using SalesHealth.Repositories.Interfaces;
using SalesHealth.Services;
using SalesHealth.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add services to the container.
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfiguration _configuration = configuration.Build();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

builder.Services.AddScoped<ISalesRepository<SaleDto>, SalesRepository<SaleDto>>();
builder.Services.AddScoped<ISalesApiAdapter<SaleDto>, SalesApiAdapter<SaleDto>>();
builder.Services.AddScoped<ISalesService, SalesService>();

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
