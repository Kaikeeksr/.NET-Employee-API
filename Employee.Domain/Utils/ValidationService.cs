using Employee.Domain.Interfaces;
using FluentValidation;

namespace Employee.Domain.Utils;

public abstract class ValidationService
{
    private readonly INotificationService _notificationService;

    protected ValidationService(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    protected void AddMessage(string message)
    {
        _notificationService.AddNotification(message);
    }
    protected async Task<bool> ExecuteValidationsAsync<TModel>(IValidator<TModel> validator, TModel model) where TModel : class
    {
        if (model is null)
        {
            AddMessage("Invalid request model data");
            return false;
        }

        
        var validatorResult = await validator.ValidateAsync(model);

        if (validatorResult.IsValid) return true;

        foreach (var error in validatorResult.Errors)
        {
            AddMessage(error.ErrorMessage);
        }

        return false;
    }
}