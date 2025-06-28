namespace Employee.Domain.Global.Values;

public class ValidDepartments
{
    public static bool IsUpToDate { get; set; } = true;
    public static Dictionary<int, string> Departments { get; set; } = new();
}