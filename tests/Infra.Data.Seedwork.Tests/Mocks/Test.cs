using System;
using Goal.Domain.Aggregates;

namespace Goal.Infra.Data.Seedwork.Tests.Mocks
{
    public class Test : Entity
    {
        public bool Active { get; set; }

        public Test(Guid id)
            : this(id, true)
        {
        }

        public Test(Guid id, bool active)
            : this()
        {
            Id = id;
            Active = active;
        }

        public Test()
            : base()
        {
        }

        public void Deactivate() => Active = false;

        public void SetId(Guid id) => Id = id;
    }
}
