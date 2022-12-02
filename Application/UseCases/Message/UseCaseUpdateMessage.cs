using Application.UseCases.Message.Dtos;
using Application.UseCases.Utils;
using Infrastructure.SqlServer.Repositories.Message;

namespace Application.UseCases.Message
{
    public class UseCaseUpdateMessage : IUpdate<OutputDtoMessage, InputDtoMessage>
    {
        private readonly IMessageRepository _messageRepository;

        public UseCaseUpdateMessage(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
            
        }

        public OutputDtoMessage Execute(int id, InputDtoMessage dto)
        {
            var messageFromDto = Mapper.GetInstance().Map<Domain.Message>(dto);

            var message = _messageRepository.Update(id,messageFromDto);

            return Mapper.GetInstance().Map<OutputDtoMessage>(message);
        }
    }
}