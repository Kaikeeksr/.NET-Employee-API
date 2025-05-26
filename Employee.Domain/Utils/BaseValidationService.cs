using Employee.Domain.Interfaces;

namespace Employee.Domain.Utils;


public abstract class BaseValidationService<TReturn, TParameters> : ValidationService, IBaseValidationService<TReturn, TParameters>
{
    protected BaseValidationService(INotificationService notificationService) : base(notificationService) { }

    public abstract Task<TReturn> Process(TParameters parameters);
}

public abstract class BaseValidationService<TParameters> : ValidationService
{
    protected BaseValidationService(INotificationService notificationService) : base(notificationService) { }

    public abstract Task Process(TParameters parameters);
}

public abstract class BaseValidationService : ValidationService
{
    protected BaseValidationService(INotificationService notificationService) : base(notificationService) { }

    public abstract Task Process();
}