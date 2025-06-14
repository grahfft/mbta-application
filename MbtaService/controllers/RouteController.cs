using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class RouteController : ControllerBase
{
    private IMbtaService mbtaService;

    public RouteController(IMbtaService mbtaService)
    {
        this.mbtaService = mbtaService;
    }

    [Route("/Routes")]
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
}