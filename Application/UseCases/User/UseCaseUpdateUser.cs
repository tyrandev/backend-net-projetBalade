using System.Security.Cryptography;
using System.Text;
using Application.UseCases.User.Dtos.Dtos;
using Application.UseCases.Utils;
using Infrastructure.SqlServer.Repositories.User;

namespace Application.UseCases.User.Dtos
{
    public class UseCaseUpdateUser : IUpdate<OutPutDtoUser, InputDtoUser>
    {
        private readonly IUserRepository _userRepository;

        public UseCaseUpdateUser(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public OutPutDtoUser Execute(int id, InputDtoUser dto)
        {
            var userFromDto = Mapper.GetInstance().Map<Domain.User>(dto);
            userFromDto.Password = ComputeSha256Hash(userFromDto.Password);
            var user = _userRepository.Update(id,userFromDto);

            return Mapper.GetInstance().Map<OutPutDtoUser>(user);
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