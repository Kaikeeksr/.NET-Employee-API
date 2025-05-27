using System;
using System.Collections.Generic;

namespace Employee.Infrastructure;

public partial class TblStatus
{
    public int IdStatus { get; set; }

    public string EStatus { get; set; } = null!;

    public string? StatusDescr { get; set; }

    public virtual ICollection<TblEmployees> TblEmployees { get; set; } = new List<TblEmployees>();
}
