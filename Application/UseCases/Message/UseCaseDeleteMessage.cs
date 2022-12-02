using Application.Services.UseCases.Utils;
using Infrastructure.SqlServer.Repositories.Message;

namespace Application.UseCases.Message
{
    public class UseCaseDeleteMessage : IDelete<bool>
    {
        private readonly IMessageRepository _messageRepository;

        public UseCaseDeleteMessage(IMessageRepository rideRepository)
        {
            _messageRepository = rideRepository;
        }
        public bool Execute(int id)
        {
            _messageRepository.Delete(id);

            return true;
        }
    }
}