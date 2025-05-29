using AutoMapper;
using Employee.Domain;
using Employee.Domain.Models.Responses;

namespace Employee.Infrastructure.Configuration;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<TblEmployees, EmployeeResponse.DisableEmployeeResponse>()
            .ForMember(dest => dest.EId, opt => opt.MapFrom(src => src.EId))
            .ForMember(dest => dest.EStatusNavigation, opt => opt.MapFrom(src => src.EStatusNavigation));
    }
}
