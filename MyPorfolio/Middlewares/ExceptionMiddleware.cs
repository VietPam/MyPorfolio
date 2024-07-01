using Serilog;
using System.Diagnostics;
using System.Net;

namespace MyPorfolio.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            //Do your own business
            Log.Error($"This is an error: {ex.Message}");
            Log.Error($"Trace: {ex.StackTrace}");
            Log.Error($"{ex.ToString()}");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        Console.WriteLine($"500 server internal error - exception: ${exception.Message}");
        return context.Response.WriteAsync($"500 server internal error - exception: ${exception.Message}");
    }
}