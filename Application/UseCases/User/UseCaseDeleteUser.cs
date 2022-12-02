using Application.Services.UseCases.Utils;
using Application.UseCases.User.Dtos.Dtos;
using Application.UseCases.Utils;
using Infrastructure.SqlServer.Repositories.User;

namespace Application.UseCases.User.Dtos
{
    public class UseCaseDeleteUser : IDelete<bool>
    {
        private readonly IUserRepository _userRepository;

        public UseCaseDeleteUser(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public bool Execute(int id)
        {
            return _userRepository.Delete(id);
        }
    }
}