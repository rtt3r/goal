using Vantage.Infra.Data;

namespace Vantage.Infra.Data.Tests.Mocks
{
    internal class GenericTestRepository : EFRepository<Test>
    {
        public GenericTestRepository(IEFUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
