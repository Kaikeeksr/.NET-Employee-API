using AutoMapper;
using Employee.Domain;
using Employee.Domain.Models.Responses;

namespace Employee.Infrastructure.Configuration;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<TblEmployees, EmployeeResponse.DisableEmployeeResponse>()
            .ForMember(dest => dest.EStatus, opt => opt.MapFrom(src => src.EStatus![0]));
    }
}
