using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

[DataObject]
public class MbtaCache : IMbtaCache
{
    private IMbtaRepository mbtaRepository;

    private Dictionary<string, List<Stop>> routes = new Dictionary<string, List<Stop>>();

    private Dictionary<string, Stop> stops = new Dictionary<string, Stop>();
    private bool isCacheLoaded = false;

    public MbtaCache(IMbtaRepository mbtaRepository)
    {
        this.mbtaRepository = mbtaRepository;
    }

    public async Task<List<string>> GetAllRoutesAsync()
    {
        if (!this.isCacheLoaded)
        {
            await this.LoadCache();
        }

        var routeIds = this.routes.Keys.ToList();
        if (routeIds.Count == 0)
        {
            var mbtaRoutes = await this.mbtaRepository.GetRoutesAsync();
            mbtaRoutes.ForEach(route =>
            {
                routes.Add(route.Id, new List<Stop>());
                routeIds.Add(route.Id);
            });
        }

        return routeIds;
    }

    public async Task<List<Stop>> GetAllStopsForRouteAsync(string routeId)
    {
        if (!this.isCacheLoaded)
        {
            await this.LoadCache();
        }

        var stopsOnRoute = new List<Stop>();

        if (!this.routes.TryGetValue(routeId, out stopsOnRoute))
        {
            stopsOnRoute = await this.LoadStops(routeId);
            this.routes.Add(routeId, stopsOnRoute);
        }

        return stopsOnRoute;
    }

    private async Task LoadCache()
    {
        var mbtaRoutes = await this.mbtaRepository.GetRoutesAsync();
        mbtaRoutes.ForEach(async route =>
        {
            var mbtaStops = await this.LoadStops(route.Id);
            this.routes.Add(route.Id, mbtaStops);
        });

        this.isCacheLoaded = true;
    }

    private async Task<List<Stop>> LoadStops(string routeId)
    {
        var mbtaStops = new List<Stop>();
        var allStops = await this.mbtaRepository.GetStopsAsync(routeId);
        allStops.ForEach(stop =>
        {
            var currentStop = stop;
            if (!this.stops.TryGetValue(stop.Id, out currentStop))
            {
                this.stops.Add(stop.Id, stop);
                currentStop = stop;
            }

            currentStop.Routes.Add(routeId);
            mbtaStops.Add(currentStop);
        });

        return mbtaStops;
    }
}