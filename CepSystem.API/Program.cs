using CepSystem.Infrastructure.Context;
using CepSystem.Infrastructure.UnitOfWork;
using CepSystem.Domain.Interfaces;
using System.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using CepSystem.Infrastructure.Services;
using CepSystem.Application.Interfaces;
using CepSystem.Infrastructure.ExternalService;
using CepSystem.Infrastructure.Repositories;
using CepSystem.Application.Services;
using Serilog;
using Microsoft.IdentityModel.Tokens;
using System.Threading.RateLimiting;



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
    builder.Services.AddScoped<IJwtService, JwtService>();
    builder.Services.AddScoped<IAuthService, AuthService>();


    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
          options.TokenValidationParameters = new TokenValidationParameters
          {
              ValidateIssuer = true,
              ValidateAudience = true,
              ValidateIssuerSigningKey = true,
              ValidateLifetime = true,


              ValidIssuer = builder.Configuration["Jwt:Issuer"],
              ValidAudience = builder.Configuration["Jwt:Audience"],

              IssuerSigningKey = new SymmetricSecurityKey(
                  Encoding.ASCII.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
          };
      });

    builder.Services.AddAuthorization();


    builder.Services.AddHttpClient<IViaCepService, ViaCepService>(client =>
    {
        var baseUrl = builder.Configuration["ViaCepOptions:BaseUrl"];

        client.BaseAddress = new Uri(baseUrl ?? "https://viacep.com.br/ws/");
    });



    builder.Services.AddRateLimiter(options =>
    {
        options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(
            httpContext => RateLimitPartition.GetFixedWindowLimiter(
               partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
               factory: partition => new FixedWindowRateLimiterOptions
               {
                   AutoReplenishment = true,
                   PermitLimit = 60,
                   QueueLimit = 0,
                   Window = TimeSpan.FromSeconds(1)
               }
            )
        );

        options.AddPolicy("login", httpContext =>
            RateLimitPartition.GetFixedWindowLimiter(
                partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknow",
                factory: partition => new FixedWindowRateLimiterOptions
                {
                    AutoReplenishment = true,
                    PermitLimit = 5,
                    QueueLimit = 0,
                    Window = TimeSpan.FromSeconds(60)
                }
            )
        );

        options.AddPolicy("register", httpContext =>
            RateLimitPartition.GetFixedWindowLimiter(
              partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknow",
              factory: partition => new FixedWindowRateLimiterOptions
              {
                  AutoReplenishment = true,
                  PermitLimit = 3,
                  QueueLimit = 0,
                  Window = TimeSpan.FromSeconds(60)
              }
            )
        );

        options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    });



    var app = builder.Build();

    app.UseRateLimiter();

    if (app.Environment.IsDevelopment())
    {

        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseAuthentication();
    app.UseAuthorization();
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
