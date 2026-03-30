using CepSystem.Infrastructure.Context;
using CepSystem.Infrastructure.UnitOfWork;
using CepSystem.Domain.Interfaces;
using System.Data;
using CepSystem.Application.Interfaces;
using CepSystem.Infrastructure.ExternalService;
using CepSystem.Infrastructure.Repositories;
using CepSystem.Application.Services;
using Serilog;

Log.Logger = new LoggerConfiguration()
  .MinimumLevel.Information()
  .WriteTo.Console()
  .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
  .CreateLogger();


try
{


    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddSingleton<DapperContext>();

    builder.Services.AddScoped<IDbConnection>(sp =>
    {
        var context = sp.GetRequiredService<DapperContext>();
        return context.CreateConnection();
    });

    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IAddressRepository, AddressRepository>();
    builder.Services.AddScoped<IAddressService, AddressService>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IUserService, UserService>();


    builder.Services.AddHttpClient<IViaCepService, ViaCepService>(client =>
    {
        var baseUrl = builder.Configuration["ViaCepOptions : BaseUrl"];

        client.BaseAddress = new Uri(baseUrl ?? "https://viacep.com.br/ws/");
    });


    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {

        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapControllers();

    app.Run();

}
catch (Exception e)
{
    Log.Fatal(e, "Application failed ");
}
finally
{
    Log.CloseAndFlush();
}
