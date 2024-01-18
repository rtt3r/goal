using FluentAssertions;
using Goal.Application.Services;
using Xunit;

namespace Goal.Seedwork.Application.Tests.Services;

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
