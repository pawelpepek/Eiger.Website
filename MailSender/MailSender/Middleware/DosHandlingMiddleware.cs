using MailSender.Exceptions;
using MailSender.Services;

namespace MailSender.Middleware;

public class DosHandlingMiddleware : IMiddleware
{
    private readonly IpService _ipService;

    public DosHandlingMiddleware(IpService ipService)
    {
        _ipService = ipService;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var ip = context.Connection.RemoteIpAddress;

        try
        {
            _ipService.IsConnectionValid(ip);
            await next.Invoke(context);
        }
        catch (ToManyConnectionsException toManyException)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync(toManyException.Message);
        }
        catch
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Coś poszło nie tak!");
        }
    }
}
