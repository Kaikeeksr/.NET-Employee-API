using System.Text.Json.Serialization;
using Employee.Domain.Global.Values;
using FluentValidation;

namespace Employee.Domain.Models.Requests;

public class EmployeeRequest
{
    public class CreateEmployeeRequest
    {
        public string EName { get; set; } = null!;
        public string ECpf { get; set; } = null!;
        public string EEmail { get; set; } = null!;
        public string? ETel { get; set; }
        public string? EGender { get; set; }
        public decimal EWage { get; set; }
        
        public int DepartmentId { get; set; }
        
        [JsonIgnore]
        public string? EStatus { get; set; }
        
        [JsonIgnore] 
        public DateTime CreatedAt { get; set; }
        
        [JsonIgnore] 
        public string ESource { get; set; } = "API .NET";
    }

    public class UpdateEmployeeRequest
    {
        public string? EName { get; set; } = null!;
        public string? ECpf { get; set; } = null!;
        public string? EEmail { get; set; } = null!;
        public string? ETel { get; set; }
        
        public int? DepartmentId { get; set; }
        public string? EGender { get; set; }
        public decimal? EWage { get; set; }
        
        [JsonIgnore] 
        public DateTime UpdatedAt { get; set; }
    }
}

public class CreateEmployeeRequestValidator : AbstractValidator<EmployeeRequest.CreateEmployeeRequest>
{
    public CreateEmployeeRequestValidator()
    {
        RuleFor(x => x.EName)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must be at most 100 characters long.");

        RuleFor(x => x.ECpf)
            .NotEmpty().WithMessage("CPF is required.")
            .Matches(@"^\d{11}$").WithMessage("CPF must contain exactly 11 numeric digits.");

        RuleFor(x => x.EEmail)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.ETel)
            .MaximumLength(20).WithMessage("Phone number must be at most 20 characters long.");

        RuleFor(x => x.DepartmentId)
            .Must(departmentId => ValidDepartments.Departments.ContainsKey(departmentId))
            .When(x => x.DepartmentId != 0)
            .WithMessage(departmentId => 
                $"Invalid department id. Allowed values: {string.Join(", ", ValidDepartments.Departments.Keys)}.");

        RuleFor(x => x.EGender)
            .Must(gender => new[] { "F", "M", "O", "N" }.Contains(gender))
            .When(x => !string.IsNullOrWhiteSpace(x.EGender))
            .WithMessage("Invalid gender. Allowed values: F, M, O, N.");

        RuleFor(x => x.EWage)
            .GreaterThan(0)
            .WithMessage("Invalid salary. Valid format example: 5575.17.")
            .When(x => x.EWage > 0);
    }
}

public class UpdateEmployeeRequestValidator : AbstractValidator<EmployeeRequest.UpdateEmployeeRequest>
{
    public UpdateEmployeeRequestValidator()
    {
        RuleFor(x => x.EName)
            .MaximumLength(100).WithMessage("Name must be at most 100 characters long.")
            .When(x => !string.IsNullOrWhiteSpace(x.EName));

        RuleFor(x => x.ECpf)
            .Matches(@"^\d{11}$").WithMessage("CPF must contain exactly 11 numeric digits.")
            .When(x => !string.IsNullOrWhiteSpace(x.ECpf));

        RuleFor(x => x.EEmail)
            .EmailAddress().WithMessage("Invalid email format.")
            .When(x => !string.IsNullOrWhiteSpace(x.EEmail));

        RuleFor(x => x.ETel)
            .MaximumLength(20).WithMessage("Phone number must be at most 20 characters long.")
            .When(x => !string.IsNullOrWhiteSpace(x.ETel));

        RuleFor(x => x.DepartmentId)
            .Must(id => id.HasValue && ValidDepartments.Departments.ContainsKey(id.Value))
            .When(x => x.DepartmentId.HasValue)
            .WithMessage(_ =>
                $"Invalid department id. Allowed values: {string.Join(", ", ValidDepartments.Departments.Keys)}.");

        RuleFor(x => x.EWage)
            .GreaterThan(0)
            .WithMessage("Invalid salary. Valid format example: 5575.17.")
            .When(x => x.EWage.HasValue);

        RuleFor(x => x)
            .Custom((request, context) =>
            {
                var hasAnyField =
                    !string.IsNullOrWhiteSpace(request.EName) ||
                    !string.IsNullOrWhiteSpace(request.ECpf) ||
                    !string.IsNullOrWhiteSpace(request.EEmail) ||
                    !string.IsNullOrWhiteSpace(request.ETel) ||
                    !string.IsNullOrWhiteSpace(request.EGender) ||
                    request.DepartmentId.HasValue ||
                    request.EWage.HasValue;

                if (!hasAnyField)
                {
                    context.AddFailure("At least one field must be provided for update.");
                }
            });
    }
}

