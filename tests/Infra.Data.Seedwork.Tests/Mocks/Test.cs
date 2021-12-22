using Vantage.Domain;

namespace Vantage.Infra.Data.Seedwork.Tests.Mocks
{
    public class Test : Entity
    {
        public bool Active { get; set; }

        public Test(long id)
            : this(id, true)
        {
        }

        public Test(long id, bool active)
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

        public void SetId(long id) => Id = id;
    }
}
