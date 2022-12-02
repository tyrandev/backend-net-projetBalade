using Application.UseCases.Comment.Dtos;
using Application.UseCases.Utils;
using Infrastructure.SqlServer.Repositories.Comment;

namespace Application.UseCases.Comment
{
    
    public class UseCaseCreateComment : IWriting<OutputDtoComment, InputDtoCreateComment>
    {
        
        private readonly ICommentRepository _commentRepository;

        public UseCaseCreateComment(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        
        public OutputDtoComment Execute(InputDtoCreateComment dto)
        {
            var commentFromDto = Mapper.GetInstance().Map<Domain.Comment>(dto);
            var commentFromDb = _commentRepository.Create(commentFromDto);

            return Mapper.GetInstance().Map<OutputDtoComment>(commentFromDb);
        }
    }
}