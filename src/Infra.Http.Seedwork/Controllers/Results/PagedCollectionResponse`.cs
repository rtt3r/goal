using Vantage.Infra.Crosscutting.Collections;

namespace Vantage.Infra.Http.Controllers.Results
{
    public abstract class PagedCollectionResponse<T> where T : class, IPagedCollection
    {
        protected PagedCollectionResponse(T source)
        {
            TotalCount = source.TotalCount;
            Items = source;
        }

        public T Items { get; set; }
        public int TotalCount { get; set; }
    }
}
