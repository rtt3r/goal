using Vantage.Domain;
using Vantage.Infra.Data;

namespace Vantage.Infra.Data.Tests.Mocks
{
    internal class TestRepository : Repository
    {
        public TestRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
