using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using NotImplementedException = System.NotImplementedException;

namespace Infrastructure.SqlServer.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        public const string TableName = "users";

        public const string ColId = "id",
            ColName = "name",
            ColEmail = "email",
            ColPassword = "password";
        
        public static readonly string ReqGetAll = $"SELECT * FROM {TableName}";
        public static readonly string ReqCreate = $"INSERT INTO {TableName}({ColName}, {ColEmail}, {ColPassword}) OUTPUT INSERTED.{ColId} VALUES(@{ColName}, @{ColEmail}, @{ColPassword})";
        public static readonly string ReqDelete = $"DELETE FROM {TableName} WHERE {ColId} = @{ColId}";
        public static readonly string ReqUpdate = $"UPDATE {TableName} set {ColName} = @{ColName},{ColEmail} = @{ColEmail}, {ColPassword}=@{ColPassword} WHERE {ColId}= @{ColId}";
        public static readonly string ReqGetById = $"SELECT * FROM {TableName} WHERE {ColId}=@{ColId}";
        public static readonly string ReqGetByNameAndPassword = $"SELECT * FROM {TableName} WHERE {ColName}=@{ColName} and {ColPassword}=@{ColPassword}";
        public static readonly string ReqGetByName = $"SELECT * FROM {TableName} WHERE {ColName}=@{ColName}";
        public static readonly string ReqGetByEmail = $"SELECT * FROM {TableName} WHERE {ColEmail}=@{ColEmail}";
        
        private readonly UserFactory _userFactory = new UserFactory();

        public List<Domain.User> GetAll()
        {
            var users = new List<Domain.User>();

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
                users.Add(_userFactory.CreateFromSqlReader(reader));
            }
            return  users;
        }

        public Domain.User Create(Domain.User user)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqCreate
            };

            command.Parameters.AddWithValue("@" + ColName, user.Name);
            command.Parameters.AddWithValue("@" + ColEmail, user.Email);
            command.Parameters.AddWithValue("@" + ColPassword, user.Password);

            user.Id = (int) command.ExecuteScalar();

            return user;
        }

        public bool Delete(int id)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqDelete
            };
            command.Parameters.AddWithValue("@" + ColId, id);

            return command.ExecuteNonQuery() > 0;
        }

        public Domain.User GetById(int id)
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
            return reader.Read() ? _userFactory.CreateFromSqlReader(reader) : null;
        }

        public Domain.User Update(int id, Domain.User user)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var selectById = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqGetById
            };
            
            selectById.Parameters.AddWithValue("@" + ColId, id);
            var users = new List<Domain.User>();
            
            var reader = selectById.ExecuteReader();

            while (reader.Read())
            {
                users.Add(_userFactory.CreateFromSqlReader(reader));
            }
            
            reader.Close();
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqUpdate
            };
            
            command.Parameters.AddWithValue("@" + ColId, id);
            command.Parameters.AddWithValue("@" + ColName, user.Name);
            command.Parameters.AddWithValue("@" + ColEmail, user.Email);
            command.Parameters.AddWithValue("@" + ColPassword, user.Password);
            command.ExecuteScalar();
            
            return new Domain.User
            {
                Id = id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            };
        }

        public Domain.User FindByNameAndPassword(string name, string password)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqGetByNameAndPassword
            };

            command.Parameters.AddWithValue("@" + ColName, name);
            command.Parameters.AddWithValue("@" + ColPassword, password);
            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return reader.Read() ? _userFactory.CreateFromSqlReader(reader) : null;
        }

        public Domain.User FindByName(string name)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqGetByName
            };

            command.Parameters.AddWithValue("@" + ColName, name);
            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return reader.Read() ? _userFactory.CreateFromSqlReader(reader) : null;
        }

        public Domain.User FindByEmail(string email)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqGetByEmail
            };

            command.Parameters.AddWithValue("@" + ColEmail, email);
            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return reader.Read() ? _userFactory.CreateFromSqlReader(reader) : null;
        }
    }
}