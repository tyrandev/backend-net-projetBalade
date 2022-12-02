using System.Collections.Generic;
using Application.Services.UseCases.Dog.DtosDog;
using Application.UseCases.Message;
using Application.UseCases.Message.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace projBaladeAPI.Controllers
{
    [ApiController]
    [Route("api/message")]
    public class MessageController :ControllerBase
    {
        private readonly UseCaseGetAllMessage _useCaseGetAllMessage;
        private readonly UseCaseCreateMessage _useCaseCreateMessage;
        private readonly UseCaseDeleteMessage _useCaseDeleteMessage;
        private readonly UseCaseGetMessageById _useCaseGetMessageById;
        private readonly UseCaseUpdateMessage _useCaseUpdateMessage;

        public MessageController(UseCaseGetAllMessage useCaseGetAllMessage, UseCaseCreateMessage useCaseCreateMessage, UseCaseDeleteMessage useCaseDeleteMessage, UseCaseGetMessageById useCaseGetMessageById, UseCaseUpdateMessage useCaseUpdateMessage)
        {
            _useCaseGetAllMessage = useCaseGetAllMessage;
            _useCaseCreateMessage = useCaseCreateMessage;
            _useCaseDeleteMessage = useCaseDeleteMessage;
            _useCaseGetMessageById = useCaseGetMessageById;
            _useCaseUpdateMessage = useCaseUpdateMessage;
        }
        
        [HttpGet]
        [Route("getall")]
        public ActionResult<List<OutputDtoMessage>> GetAll()
        {
            return _useCaseGetAllMessage.Execute();
        }
        
        [HttpPost]
        public ActionResult<OutputDtoMessage> Create([FromBody] InputDtoMessage message)
        {
            //var validationResult = _userValidator.validateCreateUser(user);
            return StatusCode(201, _useCaseCreateMessage.Execute(message));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<bool> Delete(int id)
        {
            if (_useCaseDeleteMessage.Execute(id))
            {
                return Ok();
            }

            return NotFound();

        }
        
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<OutputDtoMessage> GetById(int id)
        {
            return StatusCode(200,_useCaseGetMessageById.Execute(id));
        }
        
        [HttpPut]
        [Route("{id:int}")]
        public  ActionResult<OutputDtoMessage> Update(int id, [FromBody] InputDtoMessage message)
        {
            return StatusCode(200, _useCaseUpdateMessage.Execute(id, message));
        }
    }
}