using System;
using System.Collections.Generic;
using Application.Services.UseCases.Dog;
using Application.Services.UseCases.Dog.DtosDog;
using Domain;
using Infrastructure.SqlServer.Repositories.Dog;
using Infrastructure.SqlServer.Repositories.Dog.Exceptions;
using Microsoft.AspNetCore.Mvc;


namespace projBaladeAPI.Controllers
{
    [ApiController]
    [Route("api/dog")]
    public class DogController : ControllerBase
    {
        private readonly UseCaseGetAllDog _useCaseGetAllDog;
        private readonly UseCaseGetDog _useCaseGetDog;
        private readonly UseCaseCreateDog _useCaseCreateDog;
        private readonly UseCaseUpdateDog _useCaseUpdateDog;
        private readonly UseCaseDeleteDog _useCaseDeleteDog;
        private IDogRepository _dogRepository;

        public DogController(UseCaseGetAllDog useCaseGetAllDog, UseCaseGetDog useCaseGetDog, UseCaseCreateDog useCaseCreateDog, UseCaseUpdateDog useCaseUpdateDog, UseCaseDeleteDog useCaseDeleteDog, IDogRepository dogRepository)
        {
            _useCaseGetAllDog = useCaseGetAllDog;
            _useCaseGetDog = useCaseGetDog;
            _useCaseCreateDog = useCaseCreateDog;
            _useCaseUpdateDog = useCaseUpdateDog;
            _useCaseDeleteDog = useCaseDeleteDog;
            _dogRepository = dogRepository;
            _useCaseDeleteDog = useCaseDeleteDog;
            _useCaseUpdateDog = useCaseUpdateDog;

        }
        

        [HttpGet]
        [Route("getall")]
        public ActionResult<List<OutputDtoDog>> GetAll()
        {
            if (HttpContext.Items["User"] is User user)
            {
                int userId = user.Id;
                return _useCaseGetAllDog.Execute(userId);
            }

            return Unauthorized();
        }
        
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<OutputDtoDog> GetById(int id)
        {
            try
            {
                return StatusCode(200,_useCaseGetDog.Execute(id));
            }
            catch (DogNotFoundException e)
            {
                /*Console.WriteLine(e);
                throw;*/
                return StatusCode(404);
            }
        }
        
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(201)]
        public ActionResult<OutputDtoDog> Create([FromBody] InputDtoDog dog)
        {
            
            if (HttpContext.Items["User"] is User user)
            {
                int userId = user.Id;
                return _useCaseCreateDog.Execute(userId,dog);
            }
            return Unauthorized();
        }

        [HttpPut]
        [Route("{id:int}")]
        public  ActionResult<OutputDtoDog> Update(int id, [FromBody] InputDtoDog dog)
        {
            return StatusCode(200, _useCaseUpdateDog.Execute(id, dog));
        }
        
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<bool> Delete(int id)
        {
            if (_useCaseDeleteDog.Execute(id))
            {
                return Ok();
            }

            return NotFound();
            
            /*try
            {
                return StatusCode(200,_useCaseDeleteDog.Execute(id));
            }
            catch (DogNotFoundException e)
            {
                /*Console.WriteLine(e);
                throw;#1#
                return StatusCode(404);
            }*/
        }

    }
}