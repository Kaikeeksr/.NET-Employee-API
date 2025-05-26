using Employee.Domain.Interfaces;
using FluentValidation;

namespace Employee.Domain.Utils
{
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

        protected bool ExecuteValidations<TValidation, TModel>(TValidation validation, TModel model)
            where TValidation : AbstractValidator<TModel>
            where TModel : class
        {
            if (model is null)
            {
                AddMessage("Invalid request model data");
                return false;
            }

            var validatorResult = validation.Validate(model);

            if (validatorResult.IsValid) return true;

            foreach (var erro in validatorResult.Errors)
            {
                AddMessage(erro.ErrorMessage);
            }

            return false;
        }
    }
}