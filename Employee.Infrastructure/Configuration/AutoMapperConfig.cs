using AutoMapper;
using Employee.Domain;
using Employee.Domain.Models.Responses;

namespace Employee.Infrastructure.Configuration;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<TblEmployees, EmployeeResponse.CreateEmployeeResponse>()
            .ForMember(dest => dest.Eid, opt => opt.MapFrom(src => src.EId))
            .ForMember(dest => dest.EName, opt => opt.MapFrom(src => src.EName))
            .ForMember(dest => dest.EmployeeAlreadyExists, opt => opt.Ignore());
    }
}
