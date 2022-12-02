using System;
using System.Collections.Generic;
using Application.UseCases.User.Dtos;
using Application.UseCases.User.Dtos.Dtos;
using Application.UseCases.User.Exceptions;
using Infrastructure.SqlServer.Repositories.User;
using Infrastructure.SqlServer.Repositories.User.Exceptions;
using Microsoft.AspNetCore.Mvc;
using projBaladeAPI.Helpers;
using projBaladeAPI.Models;

namespace projBaladeAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly UseCaseGetAllUser _useCaseGetAllUser;
        private readonly UseCaseCreateUser _useCaseCreateUser;
        private readonly UseCaseDeleteUser _useCaseDeleteUser;
        private readonly UseCaseGetUser _useCaseGetUser;
        private readonly UseCaseUpdateUser _useCaseUpdateUser;
        private readonly UseCaseAuthenticateUser _useCaseAuthenticateUser;
        
        public UserController(
            UseCaseGetAllUser useCaseGetAllUser,
            UseCaseCreateUser useCaseCreateUser,
            UseCaseDeleteUser useCaseDeleteUser,
            UseCaseGetUser useCaseGetUser,
            UseCaseUpdateUser useCaseUpdateUser, UseCaseAuthenticateUser authenticateUser)
        {
            _useCaseGetAllUser = useCaseGetAllUser;
            _useCaseCreateUser = useCaseCreateUser;
            _useCaseDeleteUser = useCaseDeleteUser;
            _useCaseGetUser = useCaseGetUser;
            _useCaseUpdateUser = useCaseUpdateUser;
            _useCaseAuthenticateUser = authenticateUser;
        }
        
        [HttpGet]
        [Route("getAll")]
        public ActionResult<List<OutPutDtoUser>> GetAll()
        {

            return _useCaseGetAllUser.Execute();

        }
        
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(201)]
        public ActionResult<OutPutDtoUser> Create(InputDtoUser dto)
        {
            try
            {
                var outPutDtoUser = _useCaseCreateUser.Execute(dto);
                return StatusCode(201, outPutDtoUser);
            }
            catch (NameAlreadyUsedException e)
            {
                return StatusCode(5001);
            }
            catch (EmailAlreadyUsedException e)
            {
                return StatusCode(5002);
            }
            
        }
        
        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(201)]
        public ActionResult Delete(int id)
        {
            if (_useCaseDeleteUser.Execute(id))
            {
                return Ok();
            }

            return NotFound();
        }
        
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<OutPutDtoUser> GetById(int id)
        {
            try
            {
                return StatusCode(200,_useCaseGetUser.Execute(id));
            }
            catch (UserNotFoundException e)
            {
                
                return StatusCode(404);
            }
        }
        
        
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<OutPutDtoUser> Update(int id, [FromBody]InputDtoUser user)
        {
            try
            {
                return StatusCode(200, _useCaseUpdateUser.Execute(id, user));
            }
            catch (UserNotFoundException e)
            {
                return StatusCode(404);
            }
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _useCaseAuthenticateUser.Execute(model.Name, model.Password);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
        
        // [Authorize]
        // [HttpGet]
        // public IActionResult GetAllLoged()
        // {
        //     var users = _userService.GetAllLoged();
        //     return Ok(users);
        // }
        
        
        
    }
}