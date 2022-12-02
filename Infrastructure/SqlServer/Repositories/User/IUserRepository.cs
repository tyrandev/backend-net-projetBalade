using System.Collections.Generic;

namespace Infrastructure.SqlServer.Repositories.User
{
    public interface IUserRepository
    {
        List<Domain.User> GetAll();
        Domain.User Create(Domain.User user);

        bool Delete(int id);

        Domain.User GetById(int id);
        Domain.User Update(int id, Domain.User user);

        Domain.User FindByNameAndPassword(string name, string password);
        Domain.User FindByName(string name);
        Domain.User FindByEmail(string email);
        
        

    }
}