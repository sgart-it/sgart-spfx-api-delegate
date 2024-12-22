using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using SgartSPFxDelgateApi.DTOs;
using SgartSPFxDelgateApi.Services;

namespace SgartSPFxDelgateApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class DemoController(ILogger<DemoController> logger, MainService main) : ControllerBase
{
    //[HttpGet(Name = "nomeMetodo")]
    [HttpGet()]
    public async Task<DemoResponse> GetAsync()
    {
        try
        {
            string userName = User.Identity?.Name ?? string.Empty;

            return new DemoResponse
            {
                Now = DateTime.Now,
                Data = await main.GetAsync(userName),
                IsAuthenticated = User.Identity?.IsAuthenticated ?? false,
                UserName = userName,
                Claims = User.Claims.Select(x => new ClaimValue { Type = x.Type, Value = x.Value })
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, C.LOG_ERROR);
            throw;
        }

    }

    [HttpPost()]
    public async Task<DemoResponse> Post(DemoRequest param)
    {
        try
        {
            string userName = User.Identity?.Name ?? string.Empty;

            return new DemoResponse
            {
                Now = DateTime.Now,
                ReplyParam1 = param.Param1,
                Data = await main.GetAsync(userName),
                IsAuthenticated = User.Identity?.IsAuthenticated ?? false,
                UserName = userName,
                Claims = User.Claims.Select(x => new ClaimValue { Type = x.Type, Value = x.Value })
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, C.LOG_ERROR);
            throw;
        }
    }
}