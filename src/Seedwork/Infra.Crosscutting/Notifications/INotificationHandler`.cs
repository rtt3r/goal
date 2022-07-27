using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Goal.Seedwork.Infra.Crosscutting.Notifications
{
    public interface INotificationHandler<TNotification>
        where TNotification : INotification
    {
        Task AddNotificationAsync(TNotification notification, CancellationToken cancellationToken);
        Task<ICollection<TNotification>> GetNotificationsAsync(CancellationToken cancellationToken);
        ICollection<TNotification> GetNotifications();
        Task<bool> HasNotificationsAsync(CancellationToken cancellationToken);
        bool HasNotifications();
        Task<bool> HasInformationAsync(CancellationToken cancellationToken);
        bool HasInformation();
        Task<bool> HasInputValidationAsync(CancellationToken cancellationToken);
        bool HasInputValidation();
        Task<bool> HasDomainViolationAsync(CancellationToken cancellationToken);
        bool HasDomainViolation();
        Task<bool> HasInternalErrorAsync(CancellationToken cancellationToken);
        bool HasInternalError();
        Task<bool> HasExternalErrorAsync(CancellationToken cancellationToken);
        bool HasExternalError();
    }
}
