using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using NotImplementedException = System.NotImplementedException;

namespace Infrastructure.SqlServer.Repositories.Message
{
    public class MessageRepository : IMessageRepository
    {
        public const string TableName = "message";
        private readonly MessageFactory _messageFactory = new MessageFactory();
        public const string 
            ColId ="id",
            ColContent = "content",
            ColIdRecipient = "idRecipient",
            ColIdSender = "idSender",
            ColObject = "object";
        public static readonly string ReqGetAll = $"SELECT * FROM {TableName}";
        public static readonly string ReqGetById = $"SELECT * FROM {TableName} WHERE {ColId} = @{ColId}";
        public static readonly string ReqCreate = $"INSERT INTO {TableName}({ColContent}, {ColIdRecipient}, {ColIdSender}, {ColObject}) OUTPUT INSERTED.{ColId} VALUES(@{ColContent},@{ColIdRecipient},@{ColIdSender},@{ColObject})";
        public static readonly string ReqDelete = $"DELETE FROM {TableName} WHERE {ColId} = @{ColId}";
        public static readonly string ReqUpdate = $"UPDATE {TableName} SET {ColContent} = @{ColContent}, {ColIdRecipient} = @{ColIdRecipient}, {ColIdSender} = @{ColIdSender},  {ColObject} = @{ColObject}  WHERE {ColId} = @{ColId}";

        
        public List<Domain.Message> GetAll()
        {
            var messages = new List<Domain.Message>();

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
                messages.Add(_messageFactory.CreateFromSqlDataReader(reader));
            }

            return messages;
        }

        public Domain.Message Create(Domain.Message message)
        {
            using var connection = Database.GetConnection();
            connection.Open();
            
            var command = new SqlCommand()
            {
                Connection = connection,
                CommandText = ReqCreate
            };
            
            command.Parameters.AddWithValue("@" + ColContent, message.Content);
            command.Parameters.AddWithValue("@" + ColIdRecipient, message.IdRecipient);
            command.Parameters.AddWithValue("@" + ColIdSender, message.IdSender);
            command.Parameters.AddWithValue("@" + ColObject, message.Object);
            message.Id = (int) command.ExecuteScalar();
            
            return message;
        }

        public Domain.Message Update(int id, Domain.Message message)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqUpdate
            };
            command.Parameters.AddWithValue("@" + ColId, id);
            command.Parameters.AddWithValue("@" + ColContent, message.Content);
            command.Parameters.AddWithValue("@" + ColIdRecipient, message.IdRecipient);
            command.Parameters.AddWithValue("@" + ColIdSender, message.IdSender);
            command.Parameters.AddWithValue("@" + ColObject, message.Object);
            command.ExecuteScalar();
            return new Domain.Message()
            {
                Id = id,
                Content = message.Content,
                IdRecipient = message.IdRecipient,
                IdSender = message.IdSender ,
                Object = message.Object
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

        public Domain.Message GetById(int id)
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
            return reader.Read() ? _messageFactory.CreateFromSqlDataReader(reader) : null;
        }
    }
}