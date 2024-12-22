namespace SgartSPFxDelgateApi.Repositories;

/// <summary>
/// esempio di classe per accesso ai dati
/// </summary>
/// <param name="logger"></param>
public class DemoRepository(ILogger<DemoRepository> logger)
{
    public async Task<string> GetXXAsync(string userName)
    {
        logger.LogTrace(C.LOG_BEGIN);

        return "Valore che arriva dal DB " + userName;
    }
}
