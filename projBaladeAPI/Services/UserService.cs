/*using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Domain;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using projBaladeAPI.Helpers;
using projBaladeAPI.Models;

namespace projBaladeAPI.Services
{
 public interface IUserService
    {
        
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAllLoged();
        User GetById2(int id);
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        /*private List<User> _users = new List<User>
        {
            
            new User { Id = 1, Name = "test", Email = "User", Password = "test"}
        };#1#

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _users.SingleOrDefault(x => x.Name == model.Name && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<User> GetAllLoged()
        {
            //return _users;
        }

        public User GetById2(int id)
        {
            //return _users.FirstOrDefault(x => x.Id == id);
        }

        // helper methods

        private string GenerateJwtToken(User user)
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
    }
}*/