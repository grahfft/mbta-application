using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[Route("/Routes")]
[ApiController]
public class RouteController : ControllerBase
{
    private IMbtaService mbtaService;

    public RouteController(IMbtaService mbtaService)
    {
        this.mbtaService = mbtaService;
    }

    [HttpGet]
    public async Task<ActionResult<List<string>>> GetRoutes()
    {
        try
        {
            return await this.mbtaService.GetRoutesAsync();
        }
        catch (Exception ex)
        {
            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "Internal Server Error",
                Detail = ex.Message,
            };

            return new ObjectResult(problemDetails)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }

    [HttpGet("{routeId}/stops")]
    public async Task<ActionResult<List<Stop>>> GetStopsByRouteId(string routeId)
    {
        try
        {
            return await this.mbtaService.GetStopsByRouteIdAsync(routeId);
        }
        catch (Exception ex)
        {
            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "Internal Server Error",
                Detail = ex.Message,
            };

            return new ObjectResult(problemDetails)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }
}