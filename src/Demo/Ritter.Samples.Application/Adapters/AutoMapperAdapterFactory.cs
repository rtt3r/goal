using AutoMapper;
using Ritter.Infra.Crosscutting.Adapters;

namespace Goal.Demo.Application.Adapters
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
