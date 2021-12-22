namespace Vantage.Infra.Data.Seedwork.Tests.Mocks
{
    internal class GenericTestRepository : EFRepository<Test>
    {
        public GenericTestRepository(IEFUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
