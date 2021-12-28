using System.Threading;
using System.Threading.Tasks;
using Goal.DemoCqrs.Api.Commands;
using Goal.DemoCqrs.Api.Events;
using Goal.DemoCqrs.Domain.Aggregates.People;
using Goal.Domain;
using Goal.Domain.Bus;
using Goal.Domain.Commands;
using Goal.Domain.Notifications;
using Goal.Infra.Crosscutting.Adapters;
using MediatR;

namespace Goal.DemoCqrs.Api.CommandHandlers
{
    public class PersonCommandHandler : CommandHandler,
        IRequestHandler<AddPersonCommand, bool>,
        IRequestHandler<UpdatePersonCommand, bool>
    {
        private readonly IPersonRepository personRepository;
        private readonly ITypeAdapter typeAdapter;

        public PersonCommandHandler(
            IUnitOfWork unitOfWork,
            IBusHandler busHandler,
            INotificationHandler notificationHandler,
            IPersonRepository personRepository,
            ITypeAdapter typeAdapter)
            : base(unitOfWork, busHandler, notificationHandler)
        {
            this.personRepository = personRepository;
            this.typeAdapter = typeAdapter;
        }

        public async Task<bool> Handle(AddPersonCommand command, CancellationToken cancellationToken)
        {
            if (!command.IsValid())
            {
                NotifyValidationErrors(command);
                return false;
            }

            if (await personRepository.AnyAsync(PersonSpecifications.MatchCpf(command.Cpf)))
            {
                await busHandler.RaiseEvent(new Notification(command.MessageType, "The person cpf has already been taken."));
                return false;
            }

            var person = Person.CreatePerson(
                command.FirstName,
                command.LastName,
                command.Cpf);

            await personRepository.AddAsync(person);

            if (unitOfWork.Commit())
            {
                await busHandler.RaiseEvent(new PersonAddedEvent(
                    person.Id,
                    person.Name.FirstName,
                    person.Name.LastName,
                    person.Cpf.Number)
                );

                return true;
            }

            return false;
        }

        public async Task<bool> Handle(UpdatePersonCommand command, CancellationToken cancellationToken)
        {
            if (!command.IsValid())
            {
                NotifyValidationErrors(command);
                return false;
            }

            Person person = await personRepository.FindAsync(command.PersonId);

            if (person == null)
            {
                await busHandler.RaiseEvent(new Notification(command.MessageType, "The person has not found."));
                return false;
            }

            if (await personRepository.AnyAsync(
                !PersonSpecifications.MatchId(person.Id)
                && PersonSpecifications.MatchCpf(person.Cpf.Number)))
            {
                await busHandler.RaiseEvent(new Notification(command.MessageType, "The person cpf has already been taken."));
                return false;
            }

            personRepository.Update(person);

            if (unitOfWork.Commit())
            {
                return true;
            }

            return false;
        }

        //public Task<bool> Handle(RemoveCustomerCommand message, CancellationToken cancellationToken)
        //{
        //    if (!message.IsValid())
        //    {
        //        NotifyValidationErrors(message);
        //        return Task.FromResult(false);
        //    }

        //    _customerRepository.Remove(message.Id);

        //    if (Commit())
        //    {
        //        Bus.RaiseEvent(new CustomerRemovedEvent(message.Id));
        //    }

        //    return Task.FromResult(true);
        //}

        //public void Dispose() => _customerRepository.Dispose();
    }
}

