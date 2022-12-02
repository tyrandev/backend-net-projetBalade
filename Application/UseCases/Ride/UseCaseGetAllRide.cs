using System.Collections.Generic;
using Application.UseCases.Ride.Dtos;
using Application.UseCases.Utils;
using Infrastructure.SqlServer.Repositories.Ride;

namespace Application.UseCases.Ride
{
    public class UseCaseGetAllRide : IQuery<List<OutPutDtoRide>>
    {
        private readonly IRideRepository _rideRepository;

        public UseCaseGetAllRide(IRideRepository rideRepository)
        {
            _rideRepository = rideRepository;
        }
        

        public List<OutPutDtoRide> Execute()
        {
            var rideFromDb = _rideRepository.GetAll();

            return Mapper.GetInstance().Map<List<OutPutDtoRide>>(rideFromDb);
        }

        public List<OutPutDtoRide> Execute(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}