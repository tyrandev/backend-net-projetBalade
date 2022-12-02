namespace Domain
{
    public class Message
    {
        public int Id { get; set; }
        public int IdRecipient { get; set; }
        public int IdSender { get; set; }
        public string Content { get; set; }
        public string Object { get; set; }

        public Message()
        {
        }
    }
}