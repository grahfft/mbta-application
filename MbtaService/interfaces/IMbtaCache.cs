public interface IMbtaCache
{
    public Task<List<string>> GetAllRoutesAsync();

    public Task<List<Stop>> GetAllStopsForRouteAsync(string routeId);
}