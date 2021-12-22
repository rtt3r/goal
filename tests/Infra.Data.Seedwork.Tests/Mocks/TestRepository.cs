using Vantage.Domain;

namespace Vantage.Infra.Data.Seedwork.Tests.Mocks
{
    internal class TestRepository : Repository
    {
        public TestRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
