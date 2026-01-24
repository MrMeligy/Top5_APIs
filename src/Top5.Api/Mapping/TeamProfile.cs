using AutoMapper;
using Top5.Contracts.DTOs;
using Top5.Domain.Entities;

namespace Top5.Api.Mapping
{
    public class TeamProfile:Profile
    {
        public TeamProfile()
        {
            CreateMap<Team, TeamDto>()
                .ForMember(dest => dest.captinName,
                   opt => opt.MapFrom(src => src.captin.username))
                .ForMember(dest => dest.captinPhone,
                   opt => opt.MapFrom(src => src.captin.phone));
        }
    }
}
