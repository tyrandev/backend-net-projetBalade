using Application.UseCases.Ride.Dtos;
using Application.UseCases.Utils;
using GoogleMaps.LocationServices;
using Infrastructure.SqlServer.Repositories.Ride;

namespace Application.UseCases.Ride
{
    public class UseCaseCreateRide : IWriting<OutPutDtoRide,InputDtoRide>
    {
        private readonly IRideRepository _rideRepository;
        public GoogleLocationService locationService = new GoogleLocationService("AIzaSyAWR-Xd9CFnPNfHZehTyBOCvP-BcKRE6Fk");
       

        public UseCaseCreateRide(IRideRepository rideRepository)
        {
            _rideRepository = rideRepository;
        }
        public OutPutDtoRide Execute(InputDtoRide dto)
        {
            var rideFromDto = Mapper.GetInstance().Map<Domain.Ride>(dto);
            var point = locationService.GetLatLongFromAddress(dto.Place);
            var latitude = point.Latitude;
            var longitude = point.Longitude;
            rideFromDto.Latitude = latitude;
            rideFromDto.Longitude = longitude;
            var rideFromDb = _rideRepository.Create(0,rideFromDto);

            return Mapper.GetInstance().Map<OutPutDtoRide>(rideFromDb);
        }
        
        public OutPutDtoRide Execute(int id ,InputDtoRide dto)
        {
            var rideFromDto = Mapper.GetInstance().Map<Domain.Ride>(dto);
            var point = locationService.GetLatLongFromAddress(dto.Place);
            var latitude = point.Latitude;
            var longitude = point.Longitude;
            rideFromDto.Latitude = latitude;
            rideFromDto.Longitude = longitude;
            var rideFromDb = _rideRepository.Create(id,rideFromDto);

            return Mapper.GetInstance().Map<OutPutDtoRide>(rideFromDb);
        }
        
    }
}