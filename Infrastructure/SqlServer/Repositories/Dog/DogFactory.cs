using System.Data.SqlClient;

namespace Infrastructure.SqlServer.Repositories.Dog
{
    public class DogFactory
    {
        public Domain.Dog CreateFromSqlDataReader(SqlDataReader reader)
        {
            return new Domain.Dog 
            {
                Id = reader.GetInt32(reader.GetOrdinal(DogRepository.CoLId)),
                NameDog = reader.GetString(reader.GetOrdinal(DogRepository.CoLNameDog)),
                RaceDog = reader.GetString(reader.GetOrdinal(DogRepository.CoLRaceDog)),
                DateOfBirth = reader.GetDateTime(reader.GetOrdinal(DogRepository.CoLDateBirth)),
                IdUser = reader.GetInt32(reader.GetOrdinal(DogRepository.CoLIdUser)),
            };

        }
    }
}
