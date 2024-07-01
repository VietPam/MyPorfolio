using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;
using System.Net;

namespace MyPorfolio.Middlewares;

public static class MiddlewareExtension
{
    public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}