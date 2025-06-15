using System.Threading.Tasks;
using Moq;

namespace MbtaService.Test;

public class MbtaCacheTests
{
    private List<Route> expectedRoutes;

    private const string EXPECTED_STOP = "First_Stop_1";

    [Fact]
    public async Task LoadCache_CreatesListBasedOnRouteId()
    {
        var cache = this.createCache();
        var routeList = await cache.GetAllRoutesAsync();

        Assert.Equal(2, routeList.Count);
        expectedRoutes.ForEach(route =>
        {
            Assert.Contains(route.id, routeList);
        });
    }

    [Fact]
    public async Task LoadCache_UpdateStopsWithAdditionalRoutes()
    {
        var cache = this.createCache();

        this.expectedRoutes.ForEach(async route =>
        {
            var stopList = await cache.GetAllStopsForRouteAsync(route.id);
            var expectedStop = stopList.Find(stop => stop.id == EXPECTED_STOP);

            Assert.NotNull(expectedStop);
            Assert.Equal(2, expectedStop.Routes.Count);
        });
    }

    [Fact]
    public void LoadCache_CreatesConnectionsForStops()
    {
        var cache = this.createCache();

        this.expectedRoutes.ForEach(async route =>
        {
            var stopList = await cache.GetAllStopsForRouteAsync(route.id);
            var expectedStop = stopList.Find(stop => stop.id == EXPECTED_STOP);

            Assert.NotNull(expectedStop);
            Assert.Equal(2, expectedStop.Connections.Count);
        });
    }

     [Fact]
    public async Task LoadCache_ThrowExceptionIfUnableToFindStopLis()
    {
        var firstRoute = new Route() { id = "First" };
        var secondRoute = new Route() { id = "Second" };

        this.expectedRoutes = new List<Route>()
        {
            firstRoute,
            secondRoute
        };

        var firstRoute_stopList = new List<Stop>()
        {
            new Stop() { id = EXPECTED_STOP, attributes = new Attributes() { Latitude = 0.0, Longitude=0.0} },
            new Stop() { id = "First_Stop_2", attributes = new Attributes() { Latitude = 1.0, Longitude=1.0} }
        };

        var routeList = new List<Route>()
        {
            firstRoute,
        };

        var mockMbtaRepo = new Mock<IMbtaRepository>();
        mockMbtaRepo
            .Setup(mock => mock.GetRoutesAsync())
            .Returns(Task.FromResult(routeList));
        mockMbtaRepo
            .Setup(mock => mock.GetStopsAsync(firstRoute.id))
            .Returns(Task.FromResult(firstRoute_stopList));

        var cache = new MbtaCache(mockMbtaRepo.Object);
        await Assert.ThrowsAsync<KeyNotFoundException>(() => cache.GetAllStopsForRouteAsync(secondRoute.id));
    }

    private MbtaCache createCache()
    {
        var firstRoute = new Route() { id = "First" };
        var secondRoute = new Route() { id = "Second" };

        this.expectedRoutes = new List<Route>()
        {
            firstRoute,
            secondRoute
        };

        var firstRoute_stopList = new List<Stop>()
        {
            new Stop() { id = EXPECTED_STOP, attributes = new Attributes() { Latitude = 0.0, Longitude=0.0} },
            new Stop() { id = "First_Stop_2", attributes = new Attributes() { Latitude = 1.0, Longitude=1.0} }
        };

        var secondRoute_stopList = new List<Stop>()
        {
            new Stop() { id = EXPECTED_STOP, attributes = new Attributes() { Latitude = 0.0, Longitude=0.0} },
            new Stop() { id = "Second_Stop_2", attributes = new Attributes() { Latitude = -1.0, Longitude=-1.0} }
        };

        var routeList = new List<Route>()
        {
            firstRoute,
            secondRoute
        };

        var mockMbtaRepo = new Mock<IMbtaRepository>();
        mockMbtaRepo
            .Setup(mock => mock.GetRoutesAsync())
            .Returns(Task.FromResult(routeList));
        mockMbtaRepo
            .Setup(mock => mock.GetStopsAsync(firstRoute.id))
            .Returns(Task.FromResult(firstRoute_stopList));
        mockMbtaRepo
            .Setup(mock => mock.GetStopsAsync(secondRoute.id))
            .Returns(Task.FromResult(secondRoute_stopList));

        return new MbtaCache(mockMbtaRepo.Object);
    }
}
