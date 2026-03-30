using CepSystem.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using CepSystem.Domain.Entities;

namespace CepSystem.Infrastructure.Services
{

    public class JwtService : IJwtService
    {


        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {

            this._configuration = configuration;
        }

        public string GenerateToken(User user)
        {

            var claims = new Claim[]{
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:secretKey"]!));
            var creds = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
