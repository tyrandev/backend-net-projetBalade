namespace Application.UseCases.Comment.Dtos
{
    public class InputDtoCreateComment
    {
        public string Content { get; set; }
        public byte Score { get; set; }
        public byte Difficulty { get; set; }
        public int IdUser { get; set; }
        public int IdRide { get; set; }
    }
}