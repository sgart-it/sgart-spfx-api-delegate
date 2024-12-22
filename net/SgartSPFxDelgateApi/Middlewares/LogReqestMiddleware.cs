namespace SgartSPFxDelgateApi.Middlewares;

public class LogReqestMiddleware(ILogger<LogReqestMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            logger.LogDebug(C.LOG_REQUEST_BEGIN + " {method} {path}{query}"
                , context.Request.Method, context.Request.Path, context.Request.QueryString);

            await next(context);
        }
        catch (Exception ex)
        {
            var r = context.Request;
            logger.LogError(ex, "IsAuthenticated:{isAuthenticated}|{method} {protocol} {scheme}://{host}{path}{query}",
                context.User?.Identity?.IsAuthenticated,
                r.Method, r.Protocol, r.Scheme, r.Host, r.Path, r.QueryString);
            throw;
        }
        finally
        {
            logger.LogTrace(C.LOG_REQUEST_END);
        }
    }
}
