using System.Text.Json.Serialization;

namespace Employee.Domain;

public class TblEmployees
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Cpf { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telephone { get; set; }

    public required decimal Wage { get; set; }

    public DateTime? CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }

    public string? Status { get; set; }

    public string? Source { get; set; }
    
    public int? DepartmentId { get; set; }
    
    public int? GenderId { get; set; }

    [JsonIgnore]
    public virtual TblStatus? StatusNavigation { get; set; }
    
    [JsonIgnore]
    public virtual TblDepartments? DepartmentNavigation { get; set; }
    
    [JsonIgnore]
    public virtual TblGenders? GenderNavigation { get; set; }
}
