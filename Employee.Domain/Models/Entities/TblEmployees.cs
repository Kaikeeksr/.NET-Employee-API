using System.Text.Json.Serialization;

namespace Employee.Domain;

public partial class TblEmployees
{
    public int EId { get; set; }

    public string EName { get; set; } = null!;

    public string ECpf { get; set; } = null!;

    public string EEmail { get; set; } = null!;

    public string? ETel { get; set; }

    public string? EGender { get; set; }

    public string? EWage { get; set; }

    public DateTime? CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }

    public string? EStatus { get; set; }

    public string? EOrigem { get; set; }
    
    public int? DepartmentId { get; set; }

    [JsonIgnore]
    public virtual TblStatus? EStatusNavigation { get; set; }
    
    [JsonIgnore]
    public virtual TblDepartments? EDepartmentNavigation { get; set; }
}
