using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Goal.Seedwork.Infra.Crosscutting.Notifications
{
    public interface INotificationHandler<TNotification>
        where TNotification : class, INotification
    {
        Task AddNotificationAsync(TNotification notification, CancellationToken cancellationToken = default);
        Task<ICollection<TNotification>> GetNotificationsAsync(CancellationToken cancellationToken = default);
        Task<bool> HasNotificationsAsync(CancellationToken cancellationToken = default);
        Task<bool> HasInformationAsync(CancellationToken cancellationToken = default);
        Task<bool> HasInputValidationAsync(CancellationToken cancellationToken = default);
        Task<bool> HasDomainViolationAsync(CancellationToken cancellationToken = default);
        Task<bool> HasInternalErrorAsync(CancellationToken cancellationToken = default);
        Task<bool> HasExternalErrorAsync(CancellationToken cancellationToken = default);
        ICollection<TNotification> GetNotifications();
        bool HasNotifications();
        bool HasInformation();
        bool HasInputValidation();
        bool HasDomainViolation();
        bool HasInternalError();
        bool HasExternalError();
    }
}
