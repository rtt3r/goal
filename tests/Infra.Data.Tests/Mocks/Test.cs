using System;
using Goal.Seedwork.Domain.Aggregates;

namespace Goal.Seedwork.Infra.Data.Tests.Mocks
{
    public class Test : Entity
    {
        public bool Active { get; set; }
        public int TId { get; private set; }

        public Test(Guid id, int tId)
            : this(id, tId, true)
        {
        }

        public Test(Guid id, int tId, bool active)
            : base()
        {
            Id = id;
            Active = active;
            TId = tId;
        }

        public Test(int tId)
            : this(Guid.NewGuid(), tId)
        {
        }

        public void Deactivate() => Active = false;

        public void SetId(Guid id) => Id = id;
    }
}
