using System.Text.Json.Serialization;
using Employee.Domain.Interfaces.Services;
using FluentValidation;

namespace Employee.Domain.Models.Requests;

public class EmployeeRequest
{
    public class CreateEmployeeRequest
    {
        public string Name { get; set; } = null!;
        public string Cpf { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Telephone { get; set; }
        public int? GenderId { get; set; }
        public decimal Wage { get; set; }
        
        public int DepartmentId { get; set; }
        
        [JsonIgnore]
        public string? Status { get; set; }
        
        [JsonIgnore] 
        public DateTime CreatedAt { get; set; }
        
        [JsonIgnore] 
        public string Source { get; set; } = "API .NET";
    }

    public class UpdateEmployeeRequest
    {
        public string? Name { get; set; } = null!;
        public string? Cpf { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? Telephone { get; set; }
        public int? DepartmentId { get; set; }
        public int? GenderId { get; set; }
        public decimal? Wage { get; set; }
        
        [JsonIgnore] 
        public DateTime UpdatedAt { get; set; }
    }
}

public static class ValidationHelper
{
    private const string CACHE_KEY_DEPARTMENTS = "all-departments";
    private const string CACHE_KEY_GENDERS = "all-genders";

    public static async Task<bool> IsValidDepartmentIdAsync(
        ICachingService cache,
        IDepartmentsService deptService,
        int departmentId)
    {
        var cachedDepartments = await cache.GetAsync<List<TblDepartments>>(CACHE_KEY_DEPARTMENTS);
        if (cachedDepartments.Count > 0)
        {
            return cachedDepartments.Any(d => d.Id == departmentId);
        }
        
        var departments = await deptService.GetAllDepartments();
        
        if (departments.Count > 0)
        {
            await cache.SetAsync(CACHE_KEY_DEPARTMENTS, departments);
        }
        
        return departments?.Any(d => d.Id == departmentId) ?? false;
    }

    public static async Task<bool> IsValidGender(
        ICachingService cache,
        IGendersService genderService,
        int genderId)
    {
        if (genderId == 0) return true;

        var cachedGenders = await cache.GetAsync<List<TblGenders>>(CACHE_KEY_GENDERS);
        if (cachedGenders.Count > 0)
        {
            return cachedGenders.Any(g => g.Id == genderId);
        }
        
        var genders = await genderService.GetAllGenders();

        if (genders.Count > 0)
        {
            await cache.SetAsync(CACHE_KEY_GENDERS, genders);
        }
        
        return genders?.Any(g => g.Id == genderId) ?? false;
    }
}

public class UpdateEmployeeRequestValidator : AbstractValidator<EmployeeRequest.UpdateEmployeeRequest>
{
    private readonly ICachingService _cache;
    private readonly IDepartmentsService _deptService;
    private readonly IGendersService _genderService;

    public UpdateEmployeeRequestValidator(
        ICachingService cache,
        IDepartmentsService deptService,
        IGendersService gendersService)
    {
        _cache = cache;
        _deptService = deptService;
        _genderService = gendersService;
        
        RuleFor(x => x.Name)
            .MaximumLength(100).WithMessage("Name must be at most 100 characters long.")
            .When(x => !string.IsNullOrWhiteSpace(x.Name));

        RuleFor(x => x.Cpf)
            .Matches(@"^\d{11}$").WithMessage("CPF must contain exactly 11 numeric digits.")
            .When(x => !string.IsNullOrWhiteSpace(x.Cpf));

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Invalid email format.")
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Telephone)
            .MaximumLength(20).WithMessage("Phone number must be at most 20 characters long.")
            .When(x => !string.IsNullOrWhiteSpace(x.Telephone));

        RuleFor(x => x.DepartmentId)
           .MustAsync(async (departmentId, ct) =>
           {
               if (!departmentId.HasValue) return true;
               return await ValidationHelper.IsValidDepartmentIdAsync(_cache, _deptService, departmentId.Value);
           })
           .When(x => x.DepartmentId.HasValue)
           .WithMessage("Invalid department id. Please provide a valid department id.");
        
        RuleFor(x => x.GenderId)
            .MustAsync(async (genderId, ct) =>
            {
                if(!genderId.HasValue) return true;
                return await ValidationHelper.IsValidGender(_cache, _genderService,genderId.Value);
            }) 
            .When(x => x.GenderId.HasValue)
            .WithMessage("Invalid option. Please provide a valid gender.");

        RuleFor(x => x.Wage)
            .GreaterThan(0)
            .WithMessage("Invalid salary. Valid format example: 5575.17.")
            .When(x => x.Wage.HasValue);

        RuleFor(x => x)
            .Custom((request, context) =>
            {
                var hasAnyField =
                    !string.IsNullOrWhiteSpace(request.Name) ||
                    !string.IsNullOrWhiteSpace(request.Cpf) ||
                    !string.IsNullOrWhiteSpace(request.Email) ||
                    !string.IsNullOrWhiteSpace(request.Telephone) ||
                    request.GenderId.HasValue ||
                    request.DepartmentId.HasValue ||
                    request.Wage.HasValue;

                if (!hasAnyField)
                {
                    context.AddFailure("At least one field must be provided for update.");
                }
            });
    }
}

public class CreateEmployeeRequestValidator : AbstractValidator<EmployeeRequest.CreateEmployeeRequest>
{
    private readonly ICachingService _cache;
    private readonly IDepartmentsService _deptService; 
    private readonly IGendersService _genderService;

    public CreateEmployeeRequestValidator(
        ICachingService cache,
        IDepartmentsService deptService)
    {
        _cache = cache;
        _deptService = deptService;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must be at most 100 characters long.");

        RuleFor(x => x.Cpf)
            .NotEmpty().WithMessage("CPF is required.")
            .Matches(@"^\d{11}$").WithMessage("CPF must contain exactly 11 numeric digits.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Telephone)
            .MaximumLength(20).WithMessage("Phone number must be at most 20 characters long.");

        RuleFor(x => x.DepartmentId)
            .MustAsync(async (departmentId, ct) =>
            {
                return await ValidationHelper.IsValidDepartmentIdAsync(_cache, _deptService, departmentId);
            })
            .WithMessage("Invalid department id. Please provide a valid DepartmentId.");

        RuleFor(x => x.GenderId)
            .MustAsync(async (genderId, ct) =>
            {
                if(!genderId.HasValue) return true;
                return await ValidationHelper.IsValidGender(_cache, _genderService,genderId.Value);
            }) 
            .When(x => x.GenderId.HasValue)
            .WithMessage("Invalid option. Please provide a valid gender.");

        RuleFor(x => x.Wage)
            .GreaterThan(0)
            .WithMessage("Invalid salary. Valid format example: 5575.17.");
    }
}