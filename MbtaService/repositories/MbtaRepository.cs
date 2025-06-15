
public class MbtaRepository : IMbtaRepository
{
    public Task<List<Route>> GetRoutesAsync()
    {
        var routeList = new List<Route>()
        {
            new Route() { Id = "First" },
            new Route() { Id = "Second" }
        };

        return Task.FromResult(routeList);
    }

    public Task<List<Stop>> GetStopsAsync(string routeId)
    {
        var stopList = new List<Stop>();
        if (routeId == "First")
        {
            stopList = new List<Stop>()
            {
                new Stop() { Id = "First_Stop_1", Attributes = new Attributes() { Latitude = 0.0, Longitude=0.0} },
                new Stop() { Id = "First_Stop_2", Attributes = new Attributes() { Latitude = 1.0, Longitude=1.0} }
            };
        }
        else if (routeId == "Second")
        {
            stopList = new List<Stop>()
            {
                new Stop() { Id = "First_Stop_1", Attributes = new Attributes() { Latitude = 0.0, Longitude=0.0} },
                new Stop() { Id = "Second_Stop_2", Attributes = new Attributes() { Latitude = 1.0, Longitude=1.0} }
            };
        }

        return Task.FromResult(stopList);
    }
}