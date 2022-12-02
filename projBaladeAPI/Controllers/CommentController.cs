
using System.Collections.Generic;
using Application.UseCases.Comment;
using Application.UseCases.Comment.Dtos;
using Domain;
using Infrastructure.SqlServer.Repositories.Comment;
using Microsoft.AspNetCore.Mvc;

namespace projBaladeAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class CommentController : ControllerBase
    {
        private readonly UseCaseCreateComment _useCaseCreateComment;
        private readonly UseCaseGetAllComments _useCaseGetAllComments;
        private readonly UseCaseUpdateComment _useCaseUpdateComments;
        

        private ICommentRepository _commentRepository;
        
        public CommentController(
            UseCaseCreateComment useCaseCreateComment,
            UseCaseGetAllComments useCaseGetAllComments,
            ICommentRepository commentRepository,UseCaseUpdateComment useCaseUpdateComment)
        {
            _useCaseCreateComment = useCaseCreateComment;
            _useCaseGetAllComments = useCaseGetAllComments;
            _commentRepository = commentRepository;
            _useCaseUpdateComments = useCaseUpdateComment;
        }
        
        [HttpGet]
        [Route("rides/{idRide:int}/comments")]
        public ActionResult<List<OutputDtoComment>> GetAll(int idRide)
        {
            return _useCaseGetAllComments.Execute(idRide);
        }

        [HttpGet]
        [Route("comments/{id:int}")]
        public Comment GetById(int id)
        {
            return _commentRepository.GetById(id);
        }
        
        [HttpPost]
        [Route("comments")]
        [ProducesResponseType(201)]
        public ActionResult<OutputDtoComment> Create([FromBody] InputDtoCreateComment dto)
        {
            return StatusCode(201, _useCaseCreateComment.Execute(dto));
        }
        
        [HttpPut]
        [Route("comments/{id:int}")]
        public ActionResult<OutputDtoComment> Update(int id, [FromBody] InputDtoCreateComment comment)
        {
            return StatusCode(200, _useCaseUpdateComments.Execute(id, comment));

        }
        
        
        [HttpDelete]
        [Route("comments/{id:int}")]
        public ActionResult Delete(int id)
        {
            if (_commentRepository.Delete(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}