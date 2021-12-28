using AutoMapper;
using Goal.Infra.Crosscutting.Adapters;

namespace Goal.DemoCqrs.Api.Adapters
{
    public class AutoMapperAdapterFactory : ITypeAdapterFactory
    {
        private readonly IMapper mapper;

        public AutoMapperAdapterFactory(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ITypeAdapter Create()
        {
            return new AutoMapperAdapter(mapper);
        }
    }
}
