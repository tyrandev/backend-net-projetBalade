namespace Application.UseCases.Ride.Dtos
{
    public class InputDtoRide
    {
        public string NameRide { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public int Difficulty { get; set; }
        public string Schedule { get; set; }
        public int Score { get; set; }
        public int IdUser { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public InputDtoRide()
        {
        }
        

        
    }
}