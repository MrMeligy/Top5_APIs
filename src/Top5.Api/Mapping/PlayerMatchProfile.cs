using AutoMapper;
using Top5.Contracts.DTOs;
using Top5.Domain.Entities;

namespace Top5.Api.Mapping
{
    public class PlayerMatchProfile : Profile
    {
        public PlayerMatchProfile()
        {
            CreateMap<MatchPlayers, MatchPlayerDto>()
               .ForMember(dest => dest.teamName,
                   opt => opt.MapFrom(src => src.team.name))
               .ForMember(dest => dest.playerName,
                   opt => opt.MapFrom(src => src.player.username))
               .ForMember(dest => dest.dob,
                   opt => opt.MapFrom(src => src.player.dob))
               .ForMember(dest => dest.picUrl,
                   opt => opt.MapFrom(src => src.player.picUrl))
               .ForMember(dest => dest.kickOff,
                   opt => opt.MapFrom(src => src.match.kickOff))
               .ForMember(dest => dest.homeTeam,
                   opt => opt.MapFrom(src => src.match.homeTeam.name))
               .ForMember(dest => dest.awayTeam,
                   opt => opt.MapFrom(src => src.match.awayTeam.name))
               .ForMember(dest => dest.homeScore,
                   opt => opt.MapFrom(src => src.match.homeScore))
               .ForMember(dest => dest.awayScore,
                   opt => opt.MapFrom(src => src.match.awayScore));
            CreateMap<CreateMatchPlayerDto, MatchPlayers>();
        }
    }
}
