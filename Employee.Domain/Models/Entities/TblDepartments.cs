using System.Text.Json.Serialization;

namespace Employee.Domain;

public class TblDepartments
{
    public int Id { get; set; }
    public required string Department { get; set; }
    
    [JsonIgnore]
    public  bool IsActive { get; set; }
    [JsonIgnore]
    public ICollection<TblEmployees> Employees { get; set; }
}