using AutoMapper;
using Employee.Domain;
using Employee.Domain.Models.Requests;
using Employee.Domain.Models.Responses;

namespace Employee.Infrastructure.Configuration;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        // Create
        CreateMap<TblEmployees, EmployeeResponse.CreateEmployeeResponse>()
            .ForMember(dest => dest.Eid, opt => opt.MapFrom(src => src.EId))
            .ForMember(dest => dest.EName, opt => opt.MapFrom(src => src.EName))
            .ForMember(dest => dest.EmployeeAlreadyExists, opt => opt.Ignore());
        
        CreateMap<TblEmployees, EmployeeRequest.CreateEmployeeRequest>().ReverseMap();
        
        // Update
        CreateMap<EmployeeRequest.UpdateEmployeeRequest ,TblEmployees>()
            // Only maps if the request value isn't null ou empty string
            .ForAllMembers(opt =>
                opt.Condition((src, _, srcValue, _) =>
                    srcValue is not null &&
                    (!(srcValue is string s) || !string.IsNullOrWhiteSpace(s))
                )
            );

        CreateMap<TblEmployees, EmployeeResponse.UpdateEmployeeResponse>().ReverseMap();
    }
}
