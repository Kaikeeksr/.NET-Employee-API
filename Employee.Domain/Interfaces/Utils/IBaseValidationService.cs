namespace Employee.Domain.Interfaces;

public interface IBaseValidationService<TReturn, TParameters>
{
    Task<TReturn> Process(TParameters parameters);
}

public interface IBaseValidationService<TParameters>
{
    Task Process(TParameters parameters);
}

public interface IBaseValidationService
{
    Task Process();
}