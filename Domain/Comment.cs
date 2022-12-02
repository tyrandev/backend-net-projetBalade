namespace Domain
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public byte Score { get; set; }

        public byte Difficulty { get; set; }

        public int IdUser { get; set; }

        public int IdRide { get; set; }

        public Comment()
        {
        }
    }
}