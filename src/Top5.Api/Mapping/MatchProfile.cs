
using AutoMapper;
using Top5.Contracts.DTOs;
using Top5.Domain.Entities;

namespace Top5.Api.Mapping
{
    public class MatchProfile : Profile
    {
        public MatchProfile()
        {
            CreateMap<Match, MatchDto>()
               .ForMember(dest => dest.homeTeamName,
                   opt => opt.MapFrom(src => src.homeTeam.name))
               .ForMember(dest => dest.awayTeamName,
                   opt => opt.MapFrom(src => src.awayTeam.name))
               .ForMember(dest => dest.homePic,
                   opt => opt.MapFrom(src => src.homeTeam.picUrl))
               .ForMember(dest => dest.awayPic,
                   opt => opt.MapFrom(src => src.awayTeam.picUrl))
               .ForMember(dest => dest.id,
                   opt => opt.MapFrom(src => src.id))
               .ForMember(dest => dest.statues,
                   opt => opt.MapFrom(src => src.statues))
               .ForMember(dest => dest.kickOff,
                   opt => opt.MapFrom(src => src.kickOff));
        }
    }
}
