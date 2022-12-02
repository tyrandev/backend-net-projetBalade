using System;

namespace Domain
{
    public class Ride
    {
        public int Id { get; set; }
        public String NameRide { get; set; }
        public String Place { get; set; }
        public String Description { get; set; }
        public String Website { get; set; }
        public Byte Difficulty { get; set; }
        public String Schedule { get; set; }
        public Byte Score { get; set; }
        public int IdUser { get; set; }
        public double Longitude { get; set; }
        
        public double Latitude { get; set; }

        public Ride(int idUser)
        {
            IdUser = idUser;

        }

        public Ride()
        {
            
        }
    }
}