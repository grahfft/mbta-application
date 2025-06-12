using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class LightRailController : ControllerBase
{
    private IMbtaService mbtaService;

    public LightRailController(IMbtaService mbtaService)
    {
        this.mbtaService = mbtaService;
    }
}