using System;
using System.Threading;
using System.Threading.Tasks;
using Goal.Application.Seedwork.Handlers;
using Goal.Demo2.Api.Application.Commands.Customers;
using Goal.Demo2.Api.Application.Events;
using Goal.Demo2.Domain.Aggregates.Customers;
using Goal.Domain.Seedwork;
using Goal.Domain.Seedwork.Notifications;
using MediatR;

namespace Goal.Demo2.Api.Application.CommandHandlers
{
    public class CustomerCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewCustomerCommand, Guid>,
        IRequestHandler<UpdateCustomerCommand, bool>,
        IRequestHandler<RemoveCustomerCommand, bool>
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerCommandHandler(
            ICustomerRepository customerRepository,
            IUnitOfWork unitOfWork,
            IBusHandler busHandler,
            INotificationHandler notificationHandler)
            : base(unitOfWork, busHandler, notificationHandler)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<Guid> Handle(RegisterNewCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                await NotifyValidationErrors(message);
                return Guid.Empty;
            }

            var customer = new Customer(message.Name, message.Email, message.BirthDate);

            if (await customerRepository.GetByEmail(customer.Email) != null)
            {
                await busHandler.RaiseEvent(new Notification(message.MessageType, "The customer e-mail has already been taken."));
                return Guid.Empty;
            }

            await customerRepository.AddAsync(customer);

            if (await Commit())
            {
                await busHandler.RaiseEvent(new CustomerRegisteredEvent(customer.Id, customer.Name, customer.Email, customer.BirthDate));
                return customer.Id;
            }

            return Guid.Empty;
        }

        public async Task<bool> Handle(UpdateCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                await NotifyValidationErrors(message);
                return false;
            }

            Customer customer = await customerRepository.FindAsync(message.Id);

            if (customer is null)
            {
                await busHandler.RaiseEvent(new Notification(message.MessageType, "The customer was not found."));
                return false;
            }

            Customer existingCustomer = await customerRepository.GetByEmail(customer.Email);

            if (existingCustomer != null && !existingCustomer.Equals(customer))
            {
                await busHandler.RaiseEvent(new Notification(message.MessageType, "The customer e-mail has already been taken."));
                return false;
            }

            customer.UpdateName(message.Name);
            customer.UpdateBirthDate(message.BirthDate);
            customerRepository.Update(existingCustomer);

            if (await Commit())
            {
                await busHandler.RaiseEvent(new CustomerUpdatedEvent(customer.Id, customer.Name, customer.Email, customer.BirthDate));
                return true;
            }

            return false;
        }

        public async Task<bool> Handle(RemoveCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                await NotifyValidationErrors(message);
                return false;
            }

            Customer customer = await customerRepository.FindAsync(message.Id);

            if (customer is null)
            {
                await busHandler.RaiseEvent(new Notification(message.MessageType, "The customer was not found."));
                return false;
            }

            customerRepository.Remove(customer);

            if (await Commit())
            {
                await busHandler.RaiseEvent(new CustomerRemovedEvent(message.Id));
                return true;
            }

            return false;
        }
    }
}
