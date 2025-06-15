using System.Web;

public class MbtaRepository : IMbtaRepository
{
    private HttpClient client;

    private const string ROUTE_BASE = "https://api-v3.mbta.com/routes";

    private const string STOPS_BASE = "https://api-v3.mbta.com/stops";

    public MbtaRepository()
    {
        this.client = new HttpClient();
    }

    public async Task<List<Route>> GetRoutesAsync()
    {
        var uriBuilder = new UriBuilder(ROUTE_BASE);
        var parameters = HttpUtility.ParseQueryString(string.Empty);

        parameters["page[offset]"] = "0";
        parameters["page[limit]"] = "50";
        parameters["filter[type]"] = "0,1";
        parameters["api_key"] = Environment.GetEnvironmentVariable("MbtaApiKey");

        uriBuilder.Query = parameters.ToString();
        var uri = uriBuilder.Uri;

        var response = await this.client.GetAsync(uri);
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var routeData = System.Text.Json.JsonSerializer.Deserialize<RouteData>(jsonResponse);

        if (routeData == null)
        {
            throw new Exception("Unable to parse MBTA response");
        }

        return routeData.data;
    }

    public async Task<List<Stop>> GetStopsAsync(string routeId)
    {
        var uriBuilder = new UriBuilder(STOPS_BASE);
        var parameters = HttpUtility.ParseQueryString(string.Empty);

        parameters["page[offset]"] = "0";
        parameters["page[limit]"] = "50";
        parameters["filter[route]"] = routeId;
        parameters["api_key"] = Environment.GetEnvironmentVariable("MbtaApiKey");

        uriBuilder.Query = parameters.ToString();
        var uri = uriBuilder.Uri;

        var response = await this.client.GetAsync(uri);
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var stopData = System.Text.Json.JsonSerializer.Deserialize<StopsData>(jsonResponse);

        if (stopData == null)
        {
            throw new Exception("Unable to parse MBTA response");
        }

        return stopData.data;
    }
}