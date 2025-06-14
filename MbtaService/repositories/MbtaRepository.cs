
public class MbtaRepository : IMbtaRepository
{
    public Task<List<Route>> GetRoutesAsync(string routeId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Stop>> GetStopsAsync(string routeId)
    {
        throw new NotImplementedException();
    }
}