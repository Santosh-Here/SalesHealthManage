using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesHealth.Cores;
using SalesHealth.DbContexts;
using SalesHealth.SalesHeathRepository.Implementation;
using SalesHealth.SalesHeathRepository.Interface;
using SalesHealth.Services.Implementation;
using SalesHealth.Services.IService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add services to the container.
builder.Services.AddDbContext<SalesHealthDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbSalesHealthConnection"));
});



builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<ISalesHealthRespository, SalesHealthRespository>();


SD.SalesApiBase = builder.Configuration["ServiceUrls:SaleAPI"];

builder.Services.AddScoped<IBaseService, BaseService>();

builder.Services.AddScoped<ISalesHealthRespository, SalesHealthRespository>();

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
