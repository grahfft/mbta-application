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
        var firstRoute = new Route() { id = "First" };
        var secondRoute = new Route() { id = "Second" };

        var firstRoute_stopList = new List<Stop>()
        {
            new Stop() { id = expectedStop, attributes = new Attributes() { Latitude = 0.0, Longitude=0.0} },
            new Stop() { id = "First_Stop_2", attributes = new Attributes() { Latitude = 1.0, Longitude=1.0} }
        };

        var secondRoute_stopList = new List<Stop>()
        {
            new Stop() { id = expectedStop, attributes = new Attributes() { Latitude = 0.0, Longitude=0.0} },
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

        var mbtaCache = new MbtaCache(mockMbtaRepo.Object);
        var routeService = new RouteService(mbtaCache);
        var routeController = new RouteController(routeService);

        var actualRouteList = await routeController.GetRoutes();
        var actualStopList = await routeController.GetStopsByRouteId(actualRouteList.Value[0]);

        Assert.Equal(2, actualStopList.Value.Count);

        var actualStop = actualStopList.Value.Find(stop => stop.id.Equals(expectedStop));
        Assert.NotNull(actualStop);
        Assert.Equal(2, actualStop.Connections.Count);
        Assert.Equal(2, actualStop.Routes.Count);
    }
}
