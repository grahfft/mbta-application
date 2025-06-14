public class MbtaService : IMbtaService
{
    private IMbtaCache mbtaCache;

    public MbtaService(IMbtaCache mbtaCache)
    {
        this.mbtaCache = mbtaCache;
    }

    public async Task<List<string>> GetRoutesAsync()
    {
        return await this.mbtaCache.GetAllRoutesAsync();
    }

    public async Task<List<Stop>> GetStopsByRouteIdAsync(string routeId)
    {
        return await this.mbtaCache.GetAllStopsForRouteAsync(routeId);
    }
}