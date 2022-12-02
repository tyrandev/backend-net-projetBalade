using Application.Services.UseCases.Dog.DtosDog;
using Application.Services.UseCases.Utils;
using Application.UseCases.Utils;
using Infrastructure.SqlServer.Repositories.Dog;

namespace Application.Services.UseCases.Dog
{
    public class UseCaseCreateDog : IWriting<OutputDtoDog,InputDtoDog>
    {
        private readonly IDogRepository _dogRepository;
        
        public UseCaseCreateDog(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }
        public OutputDtoDog Execute(InputDtoDog dto)
        {
            var dogFromDto = Mapper.GetInstance().Map<Domain.Dog>(dto);

            var dog = _dogRepository.Create(0,dogFromDto);

            return Mapper.GetInstance().Map<OutputDtoDog>(dog);
        }
        
        public OutputDtoDog Execute(int idUser,InputDtoDog dto)
        {
            var dogFromDto = Mapper.GetInstance().Map<Domain.Dog>(dto);

            var dog = _dogRepository.Create(idUser,dogFromDto);

            return Mapper.GetInstance().Map<OutputDtoDog>(dog);
        }
    }
}