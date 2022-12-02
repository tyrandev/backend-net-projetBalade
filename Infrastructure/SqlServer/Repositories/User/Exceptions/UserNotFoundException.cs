using System;

namespace Infrastructure.SqlServer.Repositories.User.Exceptions
{
    public class UserNotFoundException : Exception
    {
        private int id { get; }

        public UserNotFoundException(int id)
        {
            this.id = id;
        }
    }
}