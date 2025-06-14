public interface IMbtaService
{
    public Task<List<string>> GetRoutesAsync();

    public Task<List<Stop>> GetStopsByRouteIdAsync(string routeId);
}