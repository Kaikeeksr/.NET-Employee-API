using System.Text.Json.Serialization;

namespace Employee.Domain;

public class TblDepartments
{
    public int Id { get; set; }
    public string? Department { get; set; }
    
    [JsonIgnore]
    public ICollection<TblEmployees> Employees { get; set; }
}