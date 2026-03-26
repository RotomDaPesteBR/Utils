using System.Net;
using System.Text.Json;
using LightningArc.CORS.AspNetCore;
using LightningArc.Json.Converters;
using LightningArc.OpenAPI.AspNetCore;
using LightningArc.Results;
using LightningArc.Results.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

builder.Services.AddEndpointResults(
    wrapSuccessResponses: true,
    //defaultCulture: "pt-BR",
    configureMappings: (successes, errors) =>
{
    errors.Map<Business.OrderRejectedError>(HttpStatusCode.UnprocessableEntity, "Pedido Rejeitado", "urn:api-errors:order-rejected");
});

WebApplication app = builder.Build();

app.UseStaticFiles();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.OutputErrorsList(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ErrorsList.md"));
}

app.UseCorsPolicies();

app.UseOutputCache();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
