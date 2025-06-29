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
        public string? Gender { get; set; }
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
        public string? Gender { get; set; }
        public decimal? Wage { get; set; }
        
        [JsonIgnore] 
        public DateTime UpdatedAt { get; set; }
    }
}

public static class DepartmentValidationHelper
{
    private const string CACHE_KEY = "all-departments";

    public static async Task<bool> IsValidDepartmentIdAsync(
        ICachingService cache,
        IDepartmentsService deptService,
        int departmentId)
    {
        var cachedDepartments = await cache.GetAsync<List<TblDepartments>>(CACHE_KEY);
        if (cachedDepartments.Count > 0)
        {
            return cachedDepartments.Any(d => d.Id == departmentId);
        }
        
        var departments = await deptService.GetAllDepartments();
        
        if (departments.Count > 0)
        {
            await cache.SetAsync(CACHE_KEY, departments);
        }
        
        return departments?.Any(d => d.Id == departmentId) ?? false;
    }

    public static bool IsValidGender(string? gender)
    {
        if (string.IsNullOrWhiteSpace(gender))
        {
            return true;
        }
        var validGenders = new[] { "F", "M", "O", "N" };
        return validGenders.Contains(gender, StringComparer.OrdinalIgnoreCase);
    }
}

public class UpdateEmployeeRequestValidator : AbstractValidator<EmployeeRequest.UpdateEmployeeRequest>
{
    private readonly ICachingService _cache;
    private readonly IDepartmentsService _deptService;

    public UpdateEmployeeRequestValidator(
        ICachingService cache,
        IDepartmentsService deptService)
    {
        _cache = cache;
        _deptService = deptService;
        
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
               return await DepartmentValidationHelper.IsValidDepartmentIdAsync(_cache, _deptService, departmentId.Value);
           })
           .When(x => x.DepartmentId.HasValue)
           .WithMessage("Invalid department id. Please provide a valid DepartmentId.");
        
        RuleFor(x => x.Gender)
            .Must(DepartmentValidationHelper.IsValidGender) // Reutiliza o método de gênero
            .When(x => !string.IsNullOrWhiteSpace(x.Gender))
            .WithMessage("Invalid gender. Allowed values: F, M, O, N.");

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
                    !string.IsNullOrWhiteSpace(request.Gender) ||
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
                return await DepartmentValidationHelper.IsValidDepartmentIdAsync(_cache, _deptService, departmentId);
            })
            .WithMessage("Invalid department id. Please provide a valid DepartmentId.");

        RuleFor(x => x.Gender)
            .Must(DepartmentValidationHelper.IsValidGender)
            .When(x => !string.IsNullOrWhiteSpace(x.Gender))
            .WithMessage("Invalid gender. Allowed values: F, M, O, N.");

        RuleFor(x => x.Wage)
            .GreaterThan(0)
            .WithMessage("Invalid salary. Valid format example: 5575.17.");
    }
}