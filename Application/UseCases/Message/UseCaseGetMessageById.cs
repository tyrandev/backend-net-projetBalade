using Application.UseCases.Message.Dtos;
using Application.UseCases.Utils;
using Infrastructure.SqlServer.Repositories.Message;

namespace Application.UseCases.Message
{
    public class UseCaseGetMessageById : IQuery<OutputDtoMessage>
    {
        private readonly IMessageRepository _messageRepository;

        public UseCaseGetMessageById(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public OutputDtoMessage Execute()
        {
            throw new System.NotImplementedException();
        }

        public OutputDtoMessage Execute(int id)
        {
            var message = _messageRepository.GetById(id);

            return Mapper.GetInstance().Map<OutputDtoMessage>(message);
        }
    }
}