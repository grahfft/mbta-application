using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Namotion.Reflection;

namespace MbtaService.Test;

public class FindingAllStops
{
    [Fact]
    public async Task UserWantsToFindAllStops()
    {
        var expectedStop = "First_Stop_1";
        var firstRoute = new Route() { Id = "First" };
        var secondRoute = new Route() { Id = "Second" };

        var firstRoute_stopList = new List<Stop>()
        {
            new Stop() { Id = expectedStop, Attributes = new Attributes() { Latitude = 0.0, Longitude=0.0} },
            new Stop() { Id = "First_Stop_2", Attributes = new Attributes() { Latitude = 1.0, Longitude=1.0} }
        };

        var secondRoute_stopList = new List<Stop>()
        {
            new Stop() { Id = expectedStop, Attributes = new Attributes() { Latitude = 0.0, Longitude=0.0} },
            new Stop() { Id = "Second_Stop_2", Attributes = new Attributes() { Latitude = -1.0, Longitude=-1.0} }
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
            .Setup(mock => mock.GetStopsAsync(firstRoute.Id))
            .Returns(Task.FromResult(firstRoute_stopList));
        mockMbtaRepo
            .Setup(mock => mock.GetStopsAsync(secondRoute.Id))
            .Returns(Task.FromResult(secondRoute_stopList));

        var mbtaCache = new MbtaCache(mockMbtaRepo.Object);
        var routeService = new RouteService(mbtaCache);
        var routeController = new RouteController(routeService);

        var actualRouteList = await routeController.GetRoutes();
        var actualStopList = await routeController.GetStopsByRouteId(actualRouteList.Value[0]);

        Assert.Equal(2, actualStopList.Value.Count);

        var actualStop = actualStopList.Value.Find(stop => stop.Id.Equals(expectedStop));
        Assert.NotNull(actualStop);
        Assert.Equal(2, actualStop.Connections.Count);
        Assert.Equal(2, actualStop.Routes.Count);
    }
}
