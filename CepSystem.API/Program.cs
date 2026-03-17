using CepSystem.Infrastructure.Context;
using CepSystem.Infrastructure.UnitOfWork;
using CepSystem.Domain.Interfaces;
using CepSystem.Infrastructure.Repositories;
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
    builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IAddressRepository, AddressRepository>();


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
