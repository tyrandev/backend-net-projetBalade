namespace Application.UseCases.Message.Dtos
{
    public class InputDtoMessage
    {
        public int IdRecipient { get; set; }
        public int IdSender { get; set; }
        public string Content { get; set; }
        public string Object { get; set; }

        public InputDtoMessage()
        {
        }
    }
}