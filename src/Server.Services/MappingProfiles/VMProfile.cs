using AutoMapper;
using Server.Dtos;
using Server.Entities.Models;

namespace Server.Services.MappingProfiles
{
    public class VMProfile : Profile
    {
        public VMProfile()
        {
            CreateMap<VMModel, VM>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Ip, opt => opt.MapFrom(src => src.Ip))
                .ForMember(dest => dest.Os, opt => opt.MapFrom(src => src.Os))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Critical, opt => opt.MapFrom(src => src.Critical))
                .ForMember(dest => dest.Enabled, opt => opt.MapFrom(src => src.Enabled))
                .ForMember(dest => dest.HostId, opt => opt.MapFrom(src => src.Host.Id))
                .ForMember(dest => dest.Host, opt => opt.Ignore());
        }
    }
}
