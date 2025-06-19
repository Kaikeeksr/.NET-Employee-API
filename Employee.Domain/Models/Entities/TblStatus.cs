namespace Employee.Domain;

public partial class TblStatus
{
    public int IdStatus { get; set; }

    public string Status { get; set; } = null!;

    public string? StatusDescription { get; set; }

    public virtual ICollection<TblEmployees> TblEmployees { get; set; } = new List<TblEmployees>();
}
