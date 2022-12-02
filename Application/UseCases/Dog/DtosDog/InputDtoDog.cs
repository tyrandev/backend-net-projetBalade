using System;

namespace Application.Services.UseCases.Dog.DtosDog
{
    public class InputDtoDog
    {

        public string NameDog { get; set; }
        
        public string RaceDog { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        public int IdUser { get; set; }

        public InputDtoDog()
        {
        }
    }
}