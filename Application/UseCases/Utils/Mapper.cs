using System;
using Application.UseCases.Comment.Dtos;
using Application.Services.UseCases.Dog.DtosDog;
using Application.UseCases.Message.Dtos;
using Application.UseCases.Ride.Dtos;
using Application.UseCases.User.Dtos.Dtos;
using AutoMapper;

namespace Application.UseCases.Utils
{
    public class Mapper
    {
        private static AutoMapper.Mapper _instance;

        public static AutoMapper.Mapper GetInstance()
        {
            return _instance ??= CreateMapper();
        }
        
        private static AutoMapper.Mapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // Source, Destination
                cfg.CreateMap<Domain.Ride, OutPutDtoRide>();
                cfg.CreateMap<InputDtoRide, Domain.Ride>();
                cfg.CreateMap<Domain.Dog, OutputDtoDog>();
                cfg.CreateMap<InputDtoDog, Domain.Dog>();
                cfg.CreateMap<InputDtoCreateComment, Domain.Comment>();
                cfg.CreateMap<Domain.Comment, OutputDtoComment>();
                cfg.CreateMap<Domain.Message, OutputDtoMessage>();
                cfg.CreateMap<InputDtoMessage, Domain.Message>();
                cfg.CreateMap<Domain.User, OutPutDtoUser>();
                cfg.CreateMap<InputDtoUser, Domain.User>();
                
            });
            return new AutoMapper.Mapper(config);
        }
    }
    
}