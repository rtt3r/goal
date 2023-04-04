using Microsoft.EntityFrameworkCore;

namespace Goal.Seedwork.Infra.Data.Tests.Mocks;

internal class TestRepository : Repository<Test>
{
    public TestRepository(DbContext context)
        : base(context)
    {
    }
}
