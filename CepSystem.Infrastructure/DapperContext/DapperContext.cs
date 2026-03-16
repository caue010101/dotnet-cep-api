using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace CepSystem.Infrastructure.Context
{

    public class DapperContext
    {

        private readonly IConfiguration _configuration;

        public DapperContext(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {

            return new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
