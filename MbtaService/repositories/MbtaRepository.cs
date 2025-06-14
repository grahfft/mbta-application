
public class MbtaRepository : IMbtaRepository
{
    public List<Route> GetRoutesAsync(string routeId)
    {
        return new List<Route>()
        {
            new Route() { Id = "First" },
            new Route() { Id = "Second" }
        };
    }

    public List<Stop> GetStopsAsync(string routeId)
    {
        if (routeId == "First")
        {
            return new List<Stop>()
            {
                new Stop() { Id = "First_Stop_1", attributes = new Attributes() { latitude = 0.0, longitude=0.0} },
                new Stop() { Id = "First_Stop_2", attributes = new Attributes() { latitude = 1.0, longitude=1.0} }
            };
        }
        else if (routeId == "Second")
        {
            return new List<Stop>()
            {
                new Stop() { Id = "Second_Stop_1", attributes = new Attributes() { latitude = 0.0, longitude=0.0} },
                new Stop() { Id = "Second_Stop_2", attributes = new Attributes() { latitude = 1.0, longitude=1.0} }
            };
        }

        return new List<Stop>();
    }
}