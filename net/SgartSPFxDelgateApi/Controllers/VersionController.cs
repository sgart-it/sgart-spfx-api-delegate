using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SgartSPFxDelgateApi.DTOs;

namespace SgartSPFxDelgateApi.Controllers;

[AllowAnonymous]
[ApiController]
[Route("[controller]")]
public class VersionController(ILogger<VersionController> logger) : ControllerBase
{
    //[HttpGet(Name = "")]
    [HttpGet()]
    public VersionResponse Get()
    {
        logger.LogTrace("BEGIN");

        return new VersionResponse();
    }
}