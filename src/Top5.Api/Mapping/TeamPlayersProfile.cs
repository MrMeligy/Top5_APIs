using AutoMapper;
using Top5.Contracts.DTOs;
using Top5.Domain.Entities;

namespace Top5.Api.Mapping
{
    public class TeamPlayersProfile : Profile
    {
        public TeamPlayersProfile()
        {
            // CreateMap<Source, Destination>();
            CreateMap<TeamPlayers, TeamPlayerDto>()
                .ForMember(dest => dest.playerName,
                   opt => opt.MapFrom(src => src.player.username))
                .ForMember(dest => dest.playerPicUrl,
                   opt => opt.MapFrom(src => src.player.picUrl))
            .ForMember(dest => dest.phone,
                   opt => opt.MapFrom(src => src.player.phone))
                .ForMember(dest => dest.teamName,
                   opt => opt.MapFrom(src => src.team.name))
                .ForMember(dest => dest.teamPicUrl,
                   opt => opt.MapFrom(src => src.team.picUrl));
                
        }
    }
}
