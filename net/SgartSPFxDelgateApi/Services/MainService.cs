using SgartSPFxDelgateApi.Repositories;

namespace SgartSPFxDelgateApi.Services;

public class MainService(ILogger<MainService> logger, DemoRepository demo)
{
    public async Task<string> GetAsync(string userName)
    {
        logger.LogTrace($"{nameof(MainService)}");

        return await demo.GetXXAsync(userName);
    }
}
