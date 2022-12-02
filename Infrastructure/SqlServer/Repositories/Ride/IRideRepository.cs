using System.Collections.Generic;

namespace Infrastructure.SqlServer.Repositories.Ride
{
    public interface IRideRepository
    {
            List<Domain.Ride> GetAll();
            Domain.Ride Create(int id,Domain.Ride ride);

            Domain.Ride Update(int id, Domain.Ride ride);
            void Delete(int id);
            
            Domain.Ride GetById(int id);


    }
}