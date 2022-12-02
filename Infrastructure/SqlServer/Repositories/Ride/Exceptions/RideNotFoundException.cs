using System;

namespace Application.UseCases.Ride.Exceptions
{
    public class RideNotFoundException : Exception
    {
        private int id { get; }

        public RideNotFoundException(int id)
        {
            this.id = id;
        }
        
    }
}