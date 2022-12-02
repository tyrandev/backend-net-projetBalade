using System;

namespace Infrastructure.SqlServer.Repositories.Dog.Exceptions
{
    public class DogNotFoundException : Exception
    {
        private int id { get; }

        public DogNotFoundException(int id)
        {
            this.id = id;
        }
    }
}