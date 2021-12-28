using System.Linq;
using System.Threading.Tasks;
using Goal.Application.Extensions;
using Goal.Application.Services;
using Goal.Infra.Crosscutting.Adapters;
using Goal.Infra.Crosscutting.Collections;
using Goal.Infra.Crosscutting.Exceptions;
using Goal.Infra.Crosscutting.Trying;
using Goal.Infra.Crosscutting.Validations;
using Goal.Demo.Application.DTO.People.Requests;
using Goal.Demo.Application.DTO.People.Responses;
using Goal.Demo.Domain.Aggregates.People;

namespace Goal.Demo.Application.People
{
    public class PersonAppService : AppService, IPersonAppService
    {
        private readonly IPersonRepository personRepository;
        private readonly IEntityValidator entityValidator;
        private readonly ITypeAdapter adapter;

        public PersonAppService(
            IPersonRepository personRepository,
            IEntityValidator entityValidator,
            ITypeAdapter adapter)
            : base()
        {
            this.personRepository = personRepository;
            this.entityValidator = entityValidator;
            this.adapter = adapter;
        }

        public async Task<IPagedCollection<PersonResponse>> FindPaginatedAsync(Pagination pagination)
        {
            IPagedCollection<Person> people = await personRepository.FindAsync(pagination);
            return adapter.ProjectAsPagedCollection<PersonResponse>(people);
        }

        public async Task<PersonResponse> GetPersonAsync(string id)
        {
            Person person = await personRepository.FindAsync(id)
                ?? throw new NotFoundException("Pessoa não encontrada");

            return adapter.ProjectAs<PersonResponse>(person);
        }

        public async Task<Try<ApplicationException, PersonResponse>> AddPerson(AddPersonRequest request)
        {
            ValidationResult result = entityValidator.Validate(request);

            if (!result.IsValid)
            {
                throw new BusinessException(result.Errors.First().ToString());
            }

            if (await personRepository.AnyAsync(PersonSpecifications.MatchCpf(request.Cpf)))
            {
                throw new BusinessException("CPF duplicado");
            }

            var person = Person.CreatePerson(
                request.FirstName,
                request.LastName,
                request.Cpf);

            await personRepository.AddAsync(person);

            return adapter.ProjectAs<PersonResponse>(person);
        }

        public async Task<Try<ApplicationException, PersonResponse>> UpdatePerson(string id, UpdatePersonRequest request)
        {
            return await Try.RunAsync<ApplicationException, PersonResponse>(async () =>
            {
                ValidationResult result = entityValidator.Validate(request);

                if (!result.IsValid)
                {
                    throw new BusinessException(result.Errors.First().ToString());
                }

                Person person = await personRepository.FindAsync(id)
                    ?? throw new NotFoundException("Pessoa não encontrada");

                if (await personRepository.AnyAsync(
                    !PersonSpecifications.MatchId(person.Id)
                    && PersonSpecifications.MatchCpf(person.Cpf.Number)))
                {
                    throw new BusinessException("CPF duplicado");
                }

                await personRepository.UpdateAsync(person);

                return adapter.ProjectAs<PersonResponse>(person);
            });
        }

        public async Task<Try<ApplicationException, bool>> DeletePerson(string id)
        {
            return await Try.RunAsync<ApplicationException, bool>(async () =>
            {
                Person person = await personRepository.FindAsync(id)
                    ?? throw new NotFoundException("Pessoa não encontrada");

                await personRepository.RemoveAsync(person);
                return true;
            });
        }
    }
}
