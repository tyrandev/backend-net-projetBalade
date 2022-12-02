using System.Collections.Generic;
using Application.Services.UseCases.Dog.DtosDog;
using Application.Services.UseCases.Utils;
using Application.UseCases.Utils;
using Infrastructure.SqlServer.Repositories.Dog;

namespace Application.Services.UseCases.Dog
{
    public class UseCaseGetDog : IQuery<OutputDtoDog>
    {
        private readonly IDogRepository _dogRepository;

        public UseCaseGetDog(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }

        public OutputDtoDog Execute()
        {
            throw new System.NotImplementedException();
        }

        public OutputDtoDog Execute(int id)
        {
            var dog = _dogRepository.GetById(id);

            return Mapper.GetInstance().Map<OutputDtoDog>(dog);
        }
    }

}