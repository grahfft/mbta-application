public interface IMbtaRepository
{
    public Task<List<Route>> GetRoutesAsync(string routeId = "");

    public Task<List<Stop>> GetStopsAsync(string routeId);
}