public interface IMbtaRepository
{
    public Task<List<Route>> GetRoutesAsync();

    public Task<List<Stop>> GetStopsAsync(string routeId);
}