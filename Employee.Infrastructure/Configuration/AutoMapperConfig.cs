using AutoMapper;
using Employee.Domain;
using Employee.Domain.Models.Requests;
using Employee.Domain.Models.Responses;

namespace Employee.Infrastructure.Configuration;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        // Get
        CreateMap<TblEmployees, EmployeeResponse.GetEmployeeResponse>()
            .ForMember(dest => dest.Department,
                opt => opt.MapFrom(src => src.EDepartmentNavigation != null ? src.EDepartmentNavigation.Department : ""))
            .ForMember(dest => dest.Gender,
                opt => opt.MapFrom(src => src.EGenderNavigation != null ? src.EGenderNavigation.Gender : ""))
            .ReverseMap();

        // Create
        CreateMap<TblEmployees, EmployeeResponse.CreateEmployeeResponse>()
            .ForMember(dest => dest.Eid, opt => opt.MapFrom(src => src.EId))
            .ForMember(dest => dest.EName, opt => opt.MapFrom(src => src.EName))
            .ForMember(dest => dest.EmployeeAlreadyExists, opt => opt.Ignore());
        
        CreateMap<TblEmployees, EmployeeRequest.CreateEmployeeRequest>().ReverseMap();
        
        // Update
        // Mapeia UpdateEmployeeRequest -> TblEmployees ignorando nulos e strings em branco
        CreateMap<EmployeeRequest.UpdateEmployeeRequest, TblEmployees>()
            .ForAllMembers(opt =>
                opt.Condition((src, _, srcValue, _) =>
                    srcValue != null &&
                    (!(srcValue is string s) || !string.IsNullOrWhiteSpace(s))
                )
            );

        CreateMap<TblEmployees, EmployeeResponse.UpdateEmployeeResponse>().ReverseMap();
    }
}
