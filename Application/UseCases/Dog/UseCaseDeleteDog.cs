using System;
using Application.Services.UseCases.Dog.DtosDog;
using Application.Services.UseCases.Utils;
using Application.UseCases.Utils;
using Infrastructure.SqlServer.Repositories.Dog;

namespace Application.Services.UseCases.Dog
{
    public class UseCaseDeleteDog : IDelete<bool>
    {
        private readonly IDogRepository _dogRepository;

        public UseCaseDeleteDog(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }
        

        public bool Execute(int id)
        {
            _dogRepository.Delete(id);

            return true;
        }
    }

}