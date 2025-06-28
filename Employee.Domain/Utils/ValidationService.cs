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

        protected async Task<bool> ExecuteValidations<TModel>(
            IValidator<TModel> validator,
            TModel model)
            where TModel : class
        {
            if (model is null)
            {
                AddMessage("Invalid request model data");
                return false;
            }

            var result = await validator.ValidateAsync(model);
            if (result.IsValid) return true;

            foreach (var err in result.Errors)
                AddMessage(err.ErrorMessage);

            return false;
        }
    }
}