using Application.UseCases.Ride.Dtos;
using Application.UseCases.Utils;
using Infrastructure.SqlServer.Repositories.Ride;

namespace Application.UseCases.Ride
{
    public class UseCaseGetRideById : IQuery<OutPutDtoRide>
    {
        private readonly IRideRepository _rideRepository;
        
        public UseCaseGetRideById(IRideRepository rideRepository)
        {
            _rideRepository = rideRepository;
        }
        
        public OutPutDtoRide Execute()
        {
            throw new System.NotImplementedException();
        }

        public OutPutDtoRide Execute(int id)
        {
            var ride = _rideRepository.GetById(id);

            return Mapper.GetInstance().Map<OutPutDtoRide>(ride);        }
    }
}