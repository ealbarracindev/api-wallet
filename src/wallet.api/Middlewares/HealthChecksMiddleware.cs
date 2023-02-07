using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Diagnostics;

namespace wallet.api.Middlewares;

public static class HealthChecksMiddleware
{
    public static IApplicationBuilder UseCustomHealthchecks(this IApplicationBuilder app)
    {
        app.UseHealthChecks(
            "/api/health",
            new HealthCheckOptions
            {
                Predicate = registration => registration.Name.Equals("self"),
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                AllowCachingResponses = false,
            }
         );

        app.UseHealthChecks("/api/health-dependencies", new HealthCheckOptions
        {
            Predicate = registration => registration.Tags.Contains("dependencies"),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
            AllowCachingResponses = false,
        });        

        return app;
    }

    //public static IApplicationBuilder UseHeaderDiagnostics(this IApplicationBuilder app)
    //{
    //    var listener = app.ApplicationServices.GetService<DiagnosticListener>();

    //    if (listener.IsEnabled())
    //    {
    //        return app.Use((context, next) =>
    //        {
    //            var headers = string.Join("|", context.Request.Headers.Values.Select(h => h.ToString()));
    //            listener.Write("Api.Diagnostics.Headers", new { Headers = headers, HttpContext = context });
    //            return next();
    //        });
    //    }

    //    return app;

    //}
}
