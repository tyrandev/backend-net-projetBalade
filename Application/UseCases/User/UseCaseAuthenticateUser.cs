using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.UseCases.User.Dtos.Dtos;
using Application.UseCases.Utils;
using Infrastructure.SqlServer.Repositories.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using projBaladeAPI.Helpers;
using projBaladeAPI.Models;

namespace Application.UseCases.User.Dtos
{
    public class UseCaseAuthenticateUser : IAuthenticate<AuthenticateResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;

        public UseCaseAuthenticateUser(IUserRepository userRepository, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Execute(string username, string password)
        {

            var user = _userRepository.FindByNameAndPassword(username, ComputeSha256Hash(password));

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }
        
        private string GenerateJwtToken(Domain.User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        
        static string ComputeSha256Hash(string rawData)  
        {  
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())  
            {  
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));  
  
                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();  
                for (int i = 0; i < bytes.Length; i++)  
                {  
                    builder.Append(bytes[i].ToString("x2"));  
                }  
                return builder.ToString();  
            }  
        }  
    }
}