using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using NotImplementedException = System.NotImplementedException;

namespace Infrastructure.SqlServer.Repositories.Dog
{
    public class DogRepository : IDogRepository
    {
        public const string TableName = "dog";

        public const string CoLId = "id",
            CoLNameDog = "nameDog",
            CoLRaceDog = "raceDog",
            CoLDateBirth = "dateOfBirth",
            CoLIdUser = "idUser";

        public static readonly string ReqGetAll = $"SELECT * FROM {TableName} WHERE {CoLIdUser} = @{CoLIdUser}";
        public static readonly string ReqGetById = $"SELECT * FROM {TableName} WHERE {CoLId} = @{CoLId}";
        public static readonly string ReqCreate = $"INSERT INTO {TableName}({CoLNameDog}, {CoLRaceDog}, {CoLDateBirth}, {CoLIdUser}) OUTPUT INSERTED.{CoLId} VALUES(@{CoLNameDog},@{CoLRaceDog},@{CoLDateBirth},@{CoLIdUser})";
        public static readonly string ReqUpdate = $"UPDATE {TableName} SET {CoLNameDog} = @{CoLNameDog}, {CoLRaceDog} = @{CoLRaceDog}, {CoLDateBirth} = @{CoLDateBirth},  {CoLIdUser} = @{CoLIdUser}  WHERE {CoLId} = @{CoLId}";
        public static readonly string ReqDelete = $"DELETE FROM {TableName} WHERE {CoLId} = @{CoLId}";
       
        private readonly DogFactory _dogFactory = new DogFactory();
        public List<Domain.Dog> GetAll(int idUser)
        {
            var dogs = new List<Domain.Dog>();

            using var connection = Database.GetConnection();
            connection.Open();
            
            
            var command = new SqlCommand {Connection = connection, CommandText = ReqGetAll};
            command.Parameters.AddWithValue("@" + CoLIdUser, idUser);
            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            while (reader.Read())
                dogs.Add(_dogFactory.CreateFromSqlDataReader(reader));
            return dogs;
        }

        public Domain.Dog GetById(int id)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqGetById
            };
            
            command.Parameters.AddWithValue("@" + CoLId, id);
            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return reader.Read() ? _dogFactory.CreateFromSqlDataReader(reader) : null;
        }

        public Domain.Dog Create(int idUser,Domain.Dog dog)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqCreate
            };

            command.Parameters.AddWithValue("@" + CoLNameDog, dog.NameDog);
            command.Parameters.AddWithValue("@" + CoLRaceDog, dog.RaceDog);
            command.Parameters.AddWithValue("@" + CoLDateBirth, dog.DateOfBirth);
            command.Parameters.AddWithValue("@" + CoLIdUser, idUser);
        
            return new Domain.Dog
            {
                Id = (int) command.ExecuteScalar(),
                NameDog = dog.NameDog,
                RaceDog = dog.RaceDog,
                DateOfBirth = dog.DateOfBirth ,
                IdUser = idUser
            };
        }

        public Domain.Dog Update(int id, Domain.Dog dog)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqUpdate
            };
            command.Parameters.AddWithValue("@" + CoLId, id);
            command.Parameters.AddWithValue("@" + CoLNameDog, dog.NameDog);
            command.Parameters.AddWithValue("@" + CoLRaceDog, dog.RaceDog);
            command.Parameters.AddWithValue("@" + CoLDateBirth, dog.DateOfBirth);
            command.Parameters.AddWithValue("@" + CoLIdUser, dog.IdUser);
            command.ExecuteScalar();
            return new Domain.Dog
            {
                Id = id,
                NameDog = dog.NameDog,
                RaceDog = dog.RaceDog,
                DateOfBirth = dog.DateOfBirth ,
                IdUser = dog.IdUser
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
            command.Parameters.AddWithValue("@" + CoLId, id);
            command.ExecuteScalar();
        }
    }
}