using System.Collections.Generic;
using Application.UseCases.User.Dtos.Dtos;
using Application.UseCases.Utils;
using Infrastructure.SqlServer.Repositories.User;

namespace Application.UseCases.User.Dtos
{
    public class UseCaseGetAllUser : IQuery<List<OutPutDtoUser>>
    {
        private readonly IUserRepository _userRepository;
        
        public UseCaseGetAllUser(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public List<OutPutDtoUser> Execute()
        {
            var itemFromDb = _userRepository.GetAll();

            return Mapper.GetInstance().Map<List<OutPutDtoUser>>(itemFromDb);
        }

        public List<OutPutDtoUser> Execute(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}