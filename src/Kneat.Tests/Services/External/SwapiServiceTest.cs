using Kneat.Application.Services.External;
using Xunit;

namespace Kneat.Tests.Services.External
{
    public class SwapiServiceTest : IClassFixture<Fixture>
    {

        private readonly SwapiService _swapiService;

        public SwapiServiceTest(Fixture fixture)
        {
            _swapiService = fixture.ServiceProvider.GetService(typeof(SwapiService)) as SwapiService;
        }


        [Fact]
        public void Get_StarShip_Successfully()
        {
            var item = _swapiService.GetStarShipAsync().Result;
            Assert.NotNull(item);
            Assert.True(item.Success);

        }

        /// <summary>
        /// For simulation it's need to change 'Timeout' value in testsettings.json file. Use low values
        /// </summary>
        [Fact]
        public void Get_StarShip_Timeout_Error()
        {
            var item = _swapiService.GetStarShipAsync().Result;
            Assert.NotNull(item);
            Assert.True(!item.Success);
            Assert.Contains("timeout", item.ErrorMessage);

        }

        /// <summary>
        /// For simulation it's need to change 'BaseUrl' value in testsettings.json file to invalid Url.
        /// </summary>
        [Fact]
        public void Get_StarShip_Unexpected_Error()
        {
            var item = _swapiService.GetStarShipAsync().Result;
            Assert.NotNull(item);
            Assert.True(!item.Success);
            Assert.Contains("unexpected error", item.ErrorMessage);

        }
    }
}
