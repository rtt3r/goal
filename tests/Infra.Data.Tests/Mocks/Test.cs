using System;
using Goal.Seedwork.Domain.Aggregates;

namespace Goal.Seedwork.Infra.Data.Tests.Mocks;

public class Test : Entity
{
    public bool Active { get; set; }
    public int TId { get; private set; }

    public Test(string id, int tId)
        : this(id, tId, true)
    {
    }

    public Test(string id, int tId, bool active)
        : base()
    {
        Id = id;
        Active = active;
        TId = tId;
    }

    public Test(int tId)
        : this(Guid.NewGuid().ToString(), tId)
    {
    }

    public void Deactivate() => Active = false;
}
