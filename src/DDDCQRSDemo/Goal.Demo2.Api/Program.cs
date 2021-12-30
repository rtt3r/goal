using System.Globalization;
using System.Reflection;
using Goal.Demo2.Api.Infra.Extensions;
using Goal.Demo2.Api.Infra.Swagger;
using Goal.Infra.Crosscutting.Localization;
using MediatR;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.SystemConsole.Themes;
using Swashbuckle.AspNetCore.SwaggerGen;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ElasticsearchSinkOptions ConfigureElasticSink()
{
    return new ElasticsearchSinkOptions(new Uri(builder.Configuration.GetConnectionString("Elasticsearch")))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{builder.Environment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
    };
}

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .WriteTo.Debug()
    .WriteTo.Console()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
    .WriteTo.Elasticsearch(ConfigureElasticSink())
    .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

// Add services to the container.
builder.Services.AddServices(builder.Configuration, builder.Environment);
builder.Services.AddAutoMapperTypeAdapter();
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

builder.Services
    .AddControllers(options =>
    {
        options.EnableEndpointRouting = false;
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.Formatting = Formatting.Indented;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger()
        .UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo2 API V1");
            c.DisplayRequestDuration();
            c.RoutePrefix = string.Empty;
        });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(ApplicationCultures.Portugues, ApplicationCultures.Portugues),
    SupportedCultures = new List<CultureInfo>
    {
        ApplicationCultures.Portugues,
    },
    SupportedUICultures = new List<CultureInfo>
    {
        ApplicationCultures.Portugues,
    }
});

app.MapControllers();
app.Run();
