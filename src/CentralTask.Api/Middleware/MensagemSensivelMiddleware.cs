namespace CentralTask.Api.Middleware;

public class ContentValidationMiddleware : IMiddleware
{
    public ContentValidationMiddleware()
    {

    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await next(context);
    }
}