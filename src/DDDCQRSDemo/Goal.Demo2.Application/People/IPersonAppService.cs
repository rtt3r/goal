using System.Threading.Tasks;
using Goal.Application.Seedwork.Services;
using Goal.Demo2.Application.DTO.People.Requests;
using Goal.Demo2.Application.DTO.People.Responses;
using Goal.Infra.Crosscutting.Collections;

namespace Goal.Demo2.Application.People
{
    public interface IPersonAppService : IAppService
    {
        Task<PersonResponse> AddPerson(AddPersonRequest request);
        Task<PersonResponse> UpdatePerson(string id, UpdatePersonRequest request);
        Task<bool> DeletePerson(string id);
        Task<IPagedCollection<PersonResponse>> FindPaginatedAsync(Pagination pagination);
        Task<PersonResponse> GetPersonAsync(string id);
    }
}
