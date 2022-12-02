using System;

namespace Application.Services.UseCases.Dog.DtosDog
{
    public class OutputDtoDog
    {
        public int Id { get; set; }

        public string NameDog { get; set; }
        
        public string RaceDog { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        public int IdUser { get; set; }
    }
}