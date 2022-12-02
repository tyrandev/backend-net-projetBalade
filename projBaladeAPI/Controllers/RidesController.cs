using System;
using System.Collections.Generic;
using Application.UseCases.Ride;
using Application.UseCases.Ride.Dtos;
using Application.UseCases.Ride.Exceptions;
using Domain;
using Infrastructure.SqlServer.Repositories.Ride;
using Infrastructure.SqlServer.System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using projBaladeAPI.Helpers;

namespace projBaladeAPI.Controllers
{
    [ApiController]
    [Route("api/rides")]
    public class RidesController: ControllerBase
    {
       
        private readonly UseCaseGetAllRide _useCaseGetAllRide;
        private readonly UseCaseCreateRide _caseCreateRide;
        private readonly UseCaseUpdateRide _useCaseUpdateRide;
        private readonly UseCaseDeleteRide _useCaseDeleteRide; 
        private readonly UseCaseGetRideById _useCaseGetRideById; 

        public RidesController(UseCaseGetAllRide useCaseGetAllRide, UseCaseCreateRide useCaseCreateRide,UseCaseUpdateRide updateRide, UseCaseDeleteRide useCaseDeleteRide, UseCaseGetRideById useCaseGetRideById)
        {
         
            _useCaseGetAllRide = useCaseGetAllRide;
            _caseCreateRide = useCaseCreateRide;
            _useCaseUpdateRide = updateRide;
            _useCaseDeleteRide = useCaseDeleteRide;
            _useCaseGetRideById = useCaseGetRideById;

        }

       
     
        
        [Authorize]
        [HttpGet]
        public ActionResult<List<OutPutDtoRide>> GetAll()
        {
           
            return _useCaseGetAllRide.Execute();
        }
        
        
        [HttpPost]
        public ActionResult<OutPutDtoRide> Create([FromBody] InputDtoRide ride)
        {
            if (HttpContext.Items["User"] is User user)
            {
                int userId = user.Id;
                return StatusCode(201, _caseCreateRide.Execute(userId,ride));
            }
            //var validationResult = _userValidator.validateCreateUser(user);
            return Unauthorized();
        }
        
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<OutPutDtoRide> Update(int id, [FromBody] InputDtoRide ride)
        {
            try
            {
                return StatusCode(200, _useCaseUpdateRide.Execute(id, ride));
            }
            catch (RideNotFoundException e)
            {
                return StatusCode(404);
            }
            
        }
        
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<bool> Delete(int id)
        {
            if (_useCaseDeleteRide.Execute(id))
            {
                return Ok();
            }

            return NotFound();
            
        }
        
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<OutPutDtoRide> GetById(int id)
        {
            return StatusCode(200,_useCaseGetRideById.Execute(id));
        }
    }
}