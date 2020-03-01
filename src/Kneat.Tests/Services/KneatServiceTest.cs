using Kneat.Application.Contracts.External;
using Kneat.Application.Services;
using Xunit;

namespace Kneat.Tests.Services
{
    public class KneatServiceTest : IClassFixture<Fixture>
    {

        private readonly KneatService _kneatService;

        public KneatServiceTest(Fixture fixture)
        {
            _kneatService = fixture.ServiceProvider.GetService(typeof(KneatService)) as KneatService;
        }

        [Fact]
        public void Get_Distance_Successfully()
        {
            var item = _kneatService.Process(new GetStarShipRequest { Distance = 1000000 }).Result;
            Assert.NotNull(item);
            Assert.True(item.Success);

        }
    }
}
