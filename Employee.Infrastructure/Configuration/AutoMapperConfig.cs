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
                opt => opt.MapFrom(src => src.DepartmentNavigation != null ? src.DepartmentNavigation.Department : ""))
            .ForMember(dest => dest.Gender,
                opt => opt.MapFrom(src => src.GenderNavigation != null ? src.GenderNavigation.Gender : ""))
            .ReverseMap();

        // Create
        CreateMap<TblEmployees, EmployeeResponse.CreateEmployeeResponse>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
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
