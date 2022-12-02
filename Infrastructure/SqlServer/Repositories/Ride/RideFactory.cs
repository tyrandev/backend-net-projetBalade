using System;
using System.Data.SqlClient;

namespace Infrastructure.SqlServer.Repositories.Ride
{
    public class RideFactory 
    {
        public Domain.Ride CreateFromSqlDataReader(SqlDataReader reader)
        {
            return  new Domain.Ride()
            {
                Id = reader.GetInt32(reader.GetOrdinal(RideRepository.ColId)),
                NameRide= reader.GetString(reader.GetOrdinal(RideRepository.ColNameRide)),
                Place = reader.GetString(reader.GetOrdinal(RideRepository.ColPlace)),
                Description = reader.GetString(reader.GetOrdinal(RideRepository.ColDescription)),
                Website = reader.GetString(reader.GetOrdinal(RideRepository.ColWebsite)),
                Difficulty = reader.GetByte(reader.GetOrdinal(RideRepository.ColDifficulty)),
                Schedule = reader.GetString(reader.GetOrdinal(RideRepository.ColSchedule)),
                Score = reader.GetByte(reader.GetOrdinal(RideRepository.ColScore)),
                IdUser = reader.GetInt32(reader.GetOrdinal(RideRepository.ColIdUser)),
                Longitude = reader.GetDouble(reader.GetOrdinal(RideRepository.ColLongitude)),
                Latitude = reader.GetDouble(reader.GetOrdinal(RideRepository.ColLatitude))
                
                
            };
        }
        
    }
}