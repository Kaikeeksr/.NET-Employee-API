using System.Globalization;
using System.Text.Json.Serialization;
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
        public string? EDepartment { get; set; }
        public string? EGender { get; set; }
        public string? EWage { get; set; }
        
        [JsonIgnore]
        public string? EStatus { get; set; }
        
        [JsonIgnore] 
        public DateTime CreatedAt { get; set; }
        
        [JsonIgnore] 
        public string EOrigem { get; set; } = "API .NET";
    }

    public class UpdateEmployeeRequest
    {
        public string EName { get; set; } = null!;
        public string ECpf { get; set; } = null!;
        public string EEmail { get; set; } = null!;
        public string? ETel { get; set; }
        public string? EDepartment { get; set; }
        public string? EGender { get; set; }
        public string? EWage { get; set; }
        
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

        RuleFor(x => x.EDepartment)
            .Must(BeAValidDepartment)
            .When(x => !string.IsNullOrWhiteSpace(x.EDepartment))
            .WithMessage(
                "Invalid department. Allowed values: Design, Support, Legal, Marketing, IT, Accounting, Logistics.");

        RuleFor(x => x.EGender)
            .Must(gender => new[] { "F", "M", "O", "N" }.Contains(gender))
            .When(x => !string.IsNullOrWhiteSpace(x.EGender))
            .WithMessage("Invalid gender. Allowed values: F, M, O, N.");

        RuleFor(x => x.EWage)
            .Must(BeAValidDecimal)
            .When(x => !string.IsNullOrWhiteSpace(x.EWage))
            .WithMessage("Invalid salary. Valid format example: 5575.17.");
    }

    private bool BeAValidDepartment(string? department)
    {
        var validDepartments = new[] { "Design", "Support", "Legal", "Marketing", "IT", "Accounting", "Logistics" };
        return validDepartments.Contains(department);
    }

    private bool BeAValidDecimal(string? wage)
    {
        return decimal.TryParse(wage, NumberStyles.Number, CultureInfo.InvariantCulture, out _);
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

        RuleFor(x => x.EDepartment)
            .Must(BeAValidDepartment)
            .WithMessage("Invalid department. Allowed values: Design, Support, Legal, Marketing, IT, Accounting, Logistics.")
            .When(x => !string.IsNullOrWhiteSpace(x.EDepartment));

        RuleFor(x => x.EGender)
            .Must(gender => new[] { "F", "M", "O", "N" }.Contains(gender))
            .WithMessage("Invalid gender. Allowed values: F, M, O, N.")
            .When(x => !string.IsNullOrWhiteSpace(x.EGender));

        RuleFor(x => x.EWage)
            .Must(BeAValidDecimal)
            .WithMessage("Invalid salary. Valid format example: 5575.17.")
            .When(x => !string.IsNullOrWhiteSpace(x.EWage));
    }

    private bool BeAValidDepartment(string? department)
    {
        var validDepartments = new[] { "Design", "Support", "Legal", "Marketing", "IT", "Accounting", "Logistics" };
        return validDepartments.Contains(department);
    }

    private bool BeAValidDecimal(string? wage)
    {
        return decimal.TryParse(wage, NumberStyles.Number, CultureInfo.InvariantCulture, out _);
    }
}