namespace Employee.Domain.Models.Responses;

public class ResponseBase<T> : ResponseBase
{
    public ResponseBase()
    {
        Result = new Result();
    }

    public T Data { get; set; }
}

public class ResponseBase
{
    public ResponseBase()
    {
        Result = new Result();
    }

    public Result Result { get; set; }
}

public class Result
{
    public Result()
    {
        DateTime = DateTime.Now;
        Success = true;
    }

    public bool Success { get; set; }
    public DateTime DateTime { get; set; }
    public List<string> Messages { get; set; }
}