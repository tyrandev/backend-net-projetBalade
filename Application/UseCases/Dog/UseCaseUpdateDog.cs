using Application.Services.UseCases.Dog.DtosDog;
using Application.Services.UseCases.Utils;
using Application.UseCases.Utils;
using Infrastructure.SqlServer.Repositories.Dog;

namespace Application.Services.UseCases.Dog
{
    public class UseCaseUpdateDog : IUpdate<OutputDtoDog, InputDtoDog>
    {
        private readonly IDogRepository _dogRepository;
        
        public UseCaseUpdateDog(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }
        public OutputDtoDog Execute(int id, InputDtoDog dto)
        {
            var dogFromDto = Mapper.GetInstance().Map<Domain.Dog>(dto);

            var dog = _dogRepository.Update(id,dogFromDto);

            return Mapper.GetInstance().Map<OutputDtoDog>(dog);
        }
    }
}