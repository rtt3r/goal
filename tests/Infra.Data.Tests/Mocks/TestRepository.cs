using Microsoft.EntityFrameworkCore;

namespace Goal.Seedwork.Infra.Data.Tests.Mocks;

internal class TestRepository(DbContext context) : Repository<Test>(context)
{
    public DbContext PublicContext => Context;
}
