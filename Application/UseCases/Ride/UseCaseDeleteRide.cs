using Application.Services.UseCases.Utils;
using Infrastructure.SqlServer.Repositories.Ride;

namespace Application.UseCases.Ride
{
    public class UseCaseDeleteRide : IDelete<bool>
    {
        private readonly IRideRepository _rideRepository;

        public UseCaseDeleteRide(IRideRepository rideRepository)
        {
            _rideRepository = rideRepository;
        }
        public bool Execute(int id)
        {
            _rideRepository.Delete(id);

            return true;
        }
    }
}