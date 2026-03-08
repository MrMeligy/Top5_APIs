using AutoMapper;
using Top5.Contracts.DTOs;
using Top5.Domain.Models;

namespace Top5.Api.Mapping
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile() {
            CreateMap<Player, PlayerDto>();
        }
    }
}
