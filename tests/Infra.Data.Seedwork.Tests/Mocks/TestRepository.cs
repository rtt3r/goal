using Microsoft.EntityFrameworkCore;

namespace Goal.Infra.Data.Seedwork.Tests.Mocks
{
    internal class TestRepository : Repository<Test, int>
    {
        public TestRepository(DbContext context)
            : base(context)
        {
        }
    }
}
