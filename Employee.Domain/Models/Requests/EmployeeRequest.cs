using System.Text.Json.Serialization;
using Employee.Domain.Global.Values;
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

public class CreateEmployeeRequestValidator : AbstractValidator<EmployeeRequest.CreateEmployeeRequest>
{
    public CreateEmployeeRequestValidator()
    {
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
            .Must(departmentId => ValidDepartments.Departments.ContainsKey(departmentId))
            .When(x => x.DepartmentId != 0)
            .WithMessage(departmentId => 
                $"Invalid department id. Allowed values: {string.Join(", ", ValidDepartments.Departments.Keys)}.");

        RuleFor(x => x.Gender)
            .Must(gender => new[] { "F", "M", "O", "N" }.Contains(gender))
            .When(x => !string.IsNullOrWhiteSpace(x.Gender))
            .WithMessage("Invalid gender. Allowed values: F, M, O, N.");

        RuleFor(x => x.Wage)
            .GreaterThan(0)
            .WithMessage("Invalid salary. Valid format example: 5575.17.")
            .When(x => x.Wage > 0);
    }
}

public class UpdateEmployeeRequestValidator : AbstractValidator<EmployeeRequest.UpdateEmployeeRequest>
{
    public UpdateEmployeeRequestValidator()
    {
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
            .Must(id => id.HasValue && ValidDepartments.Departments.ContainsKey(id.Value))
            .When(x => x.DepartmentId.HasValue)
            .WithMessage(_ =>
                $"Invalid department id. Allowed values: {string.Join(", ", ValidDepartments.Departments.Keys)}.");

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

