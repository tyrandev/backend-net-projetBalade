using System.Security.Cryptography;
using System.Text;
using Application.UseCases.User.Dtos.Dtos;
using Application.UseCases.User.Exceptions;
using Application.UseCases.Utils;
using Infrastructure.SqlServer.Repositories.User;

namespace Application.UseCases.User.Dtos
{
    public class UseCaseCreateUser
    {
        private readonly IUserRepository _userRepository;

        public UseCaseCreateUser(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public OutPutDtoUser Execute(InputDtoUser dto) 
        {

            if (_userRepository.FindByName(dto.Name) != null)
            {
                throw new NameAlreadyUsedException();
            }

            if (_userRepository.FindByEmail(dto.Email) != null)
            {
                throw new EmailAlreadyUsedException();
            }
            
            
            var userFromDto = Mapper.GetInstance().Map<Domain.User>(dto);

            userFromDto.Password = ComputeSha256Hash(userFromDto.Password);
            
            var userFromDb = _userRepository.Create(userFromDto);

            return Mapper.GetInstance().Map<OutPutDtoUser>(userFromDb);
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