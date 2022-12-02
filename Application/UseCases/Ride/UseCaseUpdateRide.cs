using Application.UseCases.Ride.Dtos;
using Application.UseCases.Utils;
using Infrastructure.SqlServer.Repositories.Ride;

namespace Application.UseCases.Ride
{
    public class UseCaseUpdateRide : IUpdate<OutPutDtoRide,InputDtoRide>
    {
        private readonly IRideRepository _rideRepository;

        public UseCaseUpdateRide(IRideRepository rideRepository)
        {
            _rideRepository = rideRepository;
        }
        
        public OutPutDtoRide Execute(int id, InputDtoRide dto)
        {
            var rideFromDto = Mapper.GetInstance().Map<Domain.Ride>(dto);

            var userFromDb = _rideRepository.Update(id,rideFromDto);

            return Mapper.GetInstance().Map<OutPutDtoRide>(userFromDb);
        }
    }
}