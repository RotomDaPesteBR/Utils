using System.Net;
using System.Text.Json;
using LightningArc.Utils.AspNet;
using LightningArc.Utils.AspNet.CORS;
using LightningArc.Utils.Json.Converters;
using LightningArc.Utils.OpenAPI;
using LightningArc.Utils.Results;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Configure logging providers.
//builder.Logging.ClearProviders();

//builder.Host.UseSerilog((hostContext, services, configuration) =>
//{
//    configuration
//        .ReadFrom.Configuration(hostContext.Configuration)
//        .Enrich.FromLogContext() 
//        .WriteTo.File(
//            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"logs\\log-.txt"),
//            rollingInterval: RollingInterval.Day,
//            rollOnFileSizeLimit: true,
//            outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}] [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}"
//        )
//        .WriteTo.Console(
//            outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}] [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}"
//        );
//});

//builder.Services.AddMemoryCache();

builder.Services.AddOutputCache(options =>
{
    //options.AddBasePolicy(policy => policy.Expire(TimeSpan.FromMinutes(10)));
});

builder.Services.AddCorsPolicies();

//Configure project dependency injection.
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.Converters.AddJsonConverters();
});

builder.Services.AddOpenApi(options =>
{
    options.AddSchemaTransformers();
});

builder.Services.AddEndpointResultMappers((successes, errors) =>
{
    errors.Map<Business.OrderRejectedError>(HttpStatusCode.UnprocessableEntity, "Pedido Rejeitado", "urn:api-errors:order-rejected");
});

var app = builder.Build();

app.UseStaticFiles();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //app.MapScalarApiReference(opt =>
    //{
    //    opt.Title = "HelpDesk API";
    //    opt.Theme = ScalarTheme.Default;
    //    opt.DefaultHttpClient = new(ScalarTarget.Http, ScaLightningArclient.Http11);
    //});

    app.OutputErrorsList(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ErrorsList.md"));
}

app.UseCorsPolicies();

app.UseOutputCache();

app.UseAuthorization();

app.UseEndpointResultMappers();

app.MapControllers().WithOpenApi();

app.Run();

public partial class Program { }