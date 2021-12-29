using FluentAssertions;
using Goal.Application.Seedwork.Services;
using Xunit;

namespace Goal.Application.Seedwork.Tests.Services
{
    public class AppService_Constructor
    {
        [Fact]
        public void CreateAnInstanceOfAppService()
        {
            var appService = new TestAppService();
            appService.Should().NotBeNull();
        }

        public class TestAppService : AppService
        {
            public TestAppService() : base()
            {
            }
        }
    }
}
