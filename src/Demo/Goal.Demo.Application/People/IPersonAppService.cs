using System.Threading.Tasks;
using Goal.Application.Services;
using Goal.Infra.Crosscutting.Collections;
using Goal.Infra.Crosscutting.Exceptions;
using Goal.Infra.Crosscutting.Trying;
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
