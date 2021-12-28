using System.Threading.Tasks;
using Ritter.Application.Services;
using Ritter.Infra.Crosscutting.Collections;
using Ritter.Infra.Crosscutting.Exceptions;
using Ritter.Infra.Crosscutting.Trying;
using Goal.Demo.Application.DTO.People.Requests;
using Goal.Demo.Application.DTO.People.Responses;

namespace Goal.Demo.Application.People
{
    public interface IPersonAppService : IAppService
    {
        Task<Try<ApplicationException, PersonResponse>> AddPerson(AddPersonRequest request);
        Task<Try<ApplicationException, PersonResponse>> UpdatePerson(string id, UpdatePersonRequest request);
        Task<Try<ApplicationException, bool>> DeletePerson(string id);
        Task<IPagedCollection<PersonResponse>> FindPaginatedAsync(Pagination pagination);
        Task<PersonResponse> GetPersonAsync(string id);
    }
}
