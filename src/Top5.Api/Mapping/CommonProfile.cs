using AutoMapper;
using Top5.Contracts.Helper;

namespace Top5.Api.Mapping
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            CreateMap(typeof(PaginationResponse<>), typeof(PaginationResponse<>));
        }
    }
}
