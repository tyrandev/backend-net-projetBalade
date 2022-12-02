using Application.UseCases.User.Dtos.Dtos;
using Application.UseCases.Utils;
using Infrastructure.SqlServer.Repositories.User;

namespace Application.UseCases.User.Dtos
{
    public class UseCaseGetUser: IQuery<OutPutDtoUser>
    {
        private readonly IUserRepository _userRepository;

        public UseCaseGetUser(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public OutPutDtoUser Execute()
        {
            throw new System.NotImplementedException();
        }

        public OutPutDtoUser Execute(int id)
        {
            var dog = _userRepository.GetById(id);

            return Mapper.GetInstance().Map<OutPutDtoUser>(dog);
        }
    }
}