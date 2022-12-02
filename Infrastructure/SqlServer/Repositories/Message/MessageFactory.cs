using System.Data.SqlClient;
using Infrastructure.SqlServer.Repositories.Ride;

namespace Infrastructure.SqlServer.Repositories.Message
{
    public class MessageFactory
    {
        public Domain.Message CreateFromSqlDataReader(SqlDataReader reader)
        {
            return  new Domain.Message()
            {
                Id = reader.GetInt32(reader.GetOrdinal(MessageRepository.ColId)),
                Content= reader.GetString(reader.GetOrdinal(MessageRepository.ColContent)), 
                IdRecipient = reader.GetInt32(reader.GetOrdinal(MessageRepository.ColIdRecipient)),
                IdSender = reader.GetInt32(reader.GetOrdinal(MessageRepository.ColIdSender)),
                Object = reader.GetString(reader.GetOrdinal(MessageRepository.ColObject)), 
                
            };
        }
    }
}