using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using wallet.api.Middlewares;
using wallet.infrastructure.Configurations;
using wallet.infrastructure.Extensions;
using wallet.infrastructure.Services.UriService;

var builder = WebApplication.CreateBuilder(args);


//create the logger and setup your sinks, filters and properties
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();


//after create the builder - UseSerilog
builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCustomSwagger();


// loading appsettings.json based on environment configurations
var configuration = builder.Configuration;
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env}.json", true, true);

builder.Configuration.GetSection(key: "Config").Get<Config>();

builder.Services.AddCustomDbContextsPool(builder.Configuration);

// Register Services
builder.Services.AddCustomServices();

// Register and configure CORS
builder.Services.AddCustomCors(builder.Configuration);

// Register and configure Healt Check
builder.Services.AddCustomHealthChecks(builder.Configuration);

// Register and Configure API versioning
builder.Services.AddCustomApiVersioning();

// Register Uri services
builder.Services.AddSingleton<IUriService>(o =>
{
    var accessor = o.GetRequiredService<IHttpContextAccessor>();
    var request = accessor.HttpContext.Request;
    var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
    return new UriService(uri);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseErrorHandlingMiddleware();

app.UseCustomHealthchecks();

app.MapControllers();

app.Run();
