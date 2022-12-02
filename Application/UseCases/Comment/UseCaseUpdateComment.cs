using Application.UseCases.Comment.Dtos;
using Application.UseCases.Ride.Dtos;
using Application.UseCases.Utils;
using Infrastructure.SqlServer.Repositories.Comment;
using Infrastructure.SqlServer.Repositories.Ride;

namespace Application.UseCases.Comment
{
    public class UseCaseUpdateComment : IUpdate<OutputDtoComment,InputDtoCreateComment>
    {
        private readonly ICommentRepository _commentRepository;

        public UseCaseUpdateComment(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        
        public OutputDtoComment Execute(int id, InputDtoCreateComment dto)
        {
            var commentFromDto = Mapper.GetInstance().Map<Domain.Comment>(dto);

            var commentFromDb = _commentRepository.Update(id,commentFromDto);

            return Mapper.GetInstance().Map<OutputDtoComment>(commentFromDb);
        }
    }
}