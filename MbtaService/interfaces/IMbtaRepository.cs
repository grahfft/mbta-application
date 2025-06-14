public interface IMbtaRepository
{
    public List<Route> GetRoutesAsync(string routeId = "");

    public List<Stop> GetStopsAsync(string routeId);
}