using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Application.UseCases.Ride.Exceptions;
using GoogleMaps.LocationServices;
using NotImplementedException = System.NotImplementedException;

namespace Infrastructure.SqlServer.Repositories.Ride
{
    public class RideRepository : IRideRepository

    {
        
        
        public const string TableName = "ride";
        private readonly RideFactory _rideFactory = new RideFactory();

        public const string ColId = "id",
            ColNameRide = "nameRide",
            ColPlace = "place",
            ColDescription = "description",
            ColWebsite = "website",
            ColDifficulty = "difficulty",
            ColSchedule = "schedule",
            ColScore = "score",
            ColIdUser = "idUser",
            ColLongitude = "longitude",
            ColLatitude = "latitude";

        public static readonly string ReqGetAll = $"SELECT * FROM {TableName}";
        public static readonly string ReqGetById = $"SELECT * FROM {TableName} WHERE {ColId} = @{ColId}";
        public static readonly string ReqCreateRide = $"INSERT INTO {TableName}({ColNameRide}, {ColPlace}, {ColDescription}, {ColWebsite}, {ColDifficulty}, {ColSchedule}, {ColScore}, {ColIdUser}, {ColLatitude}, {ColLongitude}) OUTPUT INSERTED.{ColId} VALUES(@{ColNameRide}, @{ColPlace}, @{ColDescription}, @{ColWebsite}, @{ColDifficulty}, @{ColSchedule}, @{ColScore}, @{ColIdUser}, @{ColLatitude}, @{ColLongitude})";
        
        public static readonly string ReqUpdate = $"UPDATE {TableName} SET {ColNameRide} = @{ColNameRide}, {ColPlace} = @{ColPlace}, {ColDescription} = @{ColDescription}, {ColWebsite} = @{ColWebsite}, {ColDifficulty} = @{ColDifficulty}, {ColSchedule} = @{ColSchedule}, {ColScore} = @{ColScore}, {ColIdUser} = @{ColIdUser}, {ColLatitude} = @{ColLatitude}, {ColLongitude} = @{ColLongitude} WHERE {ColId} = @{ColId}";
        public static readonly string ReqDelete = $"DELETE FROM {TableName} WHERE {ColId} = @{ColId}";


        public List<Domain.Ride> GetAll()
        {
            var rides = new List<Domain.Ride>();

            using var connection = Database.GetConnection();
            connection.Open();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqGetAll

            };
            
            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                rides.Add(_rideFactory.CreateFromSqlDataReader(reader));
            }

            return rides;
        }

        public Domain.Ride Create(int id,Domain.Ride ride)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqCreateRide
            };
            
            command.Parameters.AddWithValue("@" + ColNameRide,ride.NameRide);
            command.Parameters.AddWithValue("@" + ColPlace,ride.Place);
            command.Parameters.AddWithValue("@" + ColDescription,ride.Description);
            command.Parameters.AddWithValue("@" + ColWebsite,ride.Website);
            command.Parameters.AddWithValue("@" + ColDifficulty,ride.Difficulty);
            command.Parameters.AddWithValue("@" + ColSchedule,ride.Schedule);
            command.Parameters.AddWithValue("@" + ColScore,ride.Score);
            command.Parameters.AddWithValue("@" + ColIdUser, id);
            command.Parameters.AddWithValue("@" + ColLatitude, ride.Latitude);
            command.Parameters.AddWithValue("@" + ColLongitude, ride.Longitude);
            
            return new Domain.Ride
            {
                Id = (int) command.ExecuteScalar(),
                NameRide = ride.NameRide,
                Place = ride.Place,
                Latitude = ride.Latitude,
                Longitude = ride.Longitude,
                Description =ride.Description,
                Website=ride.Website,
                Difficulty=ride.Difficulty,
                Schedule = ride.Schedule,
                Score = ride.Score,
                IdUser = id,
            };
        }

        public Domain.Ride Update(int id, Domain.Ride ride)
        {
            
            using var connection = Database.GetConnection();
            connection.Open();

            var selectById = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqGetById
            };
            
            selectById.Parameters.AddWithValue("@" + ColId, id);
            var rides = new List<Domain.Ride>();
            
            var reader = selectById.ExecuteReader();

            while (reader.Read())
            {
                rides.Add(_rideFactory.CreateFromSqlDataReader(reader));
            }
            
            reader.Close();

            if (rides.Count == 0)
            {
                throw new RideNotFoundException(id);
            }
            
            
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqUpdate
            };
            
            command.Parameters.AddWithValue("@" + ColId, id);
            command.Parameters.AddWithValue("@" + ColNameRide, ride.NameRide);
            command.Parameters.AddWithValue("@" + ColPlace, ride.Place);
            command.Parameters.AddWithValue("@" + ColDescription, ride.Description);
            command.Parameters.AddWithValue("@" + ColWebsite, ride.Website);
            command.Parameters.AddWithValue("@" + ColDifficulty, ride.Difficulty);
            command.Parameters.AddWithValue("@" + ColSchedule, ride.Schedule);
            command.Parameters.AddWithValue("@" + ColScore, ride.Score);
            command.Parameters.AddWithValue("@" + ColIdUser, ride.IdUser);
            command.Parameters.AddWithValue("@" + ColLatitude, ride.Latitude);
            command.Parameters.AddWithValue("@" + ColLongitude, ride.Longitude);
            command.ExecuteScalar();
            
            return new Domain.Ride
            {
                Id = id,
                NameRide = ride.NameRide,
                Place = ride.Place,
                Description =ride.Description,
                Website=ride.Website,
                Difficulty=ride.Difficulty,
                Schedule = ride.Schedule,
                Score = ride.Score,
                IdUser = ride.IdUser,
                Latitude = ride.Latitude,
                Longitude = ride.Longitude
            };
        }

        public void Delete(int id)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqDelete
            };
            command.Parameters.AddWithValue("@" + ColId, id);
            command.ExecuteScalar();
        }

        public Domain.Ride GetById(int id)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqGetById
            };
            
            command.Parameters.AddWithValue("@" + ColId, id);
            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return reader.Read() ? _rideFactory.CreateFromSqlDataReader(reader) : null;
        }
    }
}