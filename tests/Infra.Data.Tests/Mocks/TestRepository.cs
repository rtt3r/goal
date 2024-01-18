using Microsoft.EntityFrameworkCore;

namespace Goal.Infra.Data.Tests.Mocks;

internal class TestRepository(DbContext context) : Repository<Test>(context)
{
    public DbContext PublicContext => Context;
}
