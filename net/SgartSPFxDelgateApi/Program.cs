using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using NLog;
using NLog.Web;
using SgartSPFxDelgateApi;
using SgartSPFxDelgateApi.Middlewares;
using SgartSPFxDelgateApi.Repositories;
using SgartSPFxDelgateApi.Services;
using SgartSPFxDelgateApi.Settings;

Logger logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    logger.Info($"{C.LOG_START}: v. {C.VERSION}.{C.VERSION_BUILD}");

    var builder = WebApplication.CreateBuilder(args);

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // settings dell'applicazione
    var settingsSection = builder.Configuration.GetSection(AppSettings.KEY_NAME);
    builder.Services.Configure<AppSettings>(settingsSection);

    // Add services to the container.
    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddMicrosoftIdentityWebApi(builder.Configuration, "AzureAd");

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // project services
    builder.Services.AddScoped<DemoRepository>();
    builder.Services.AddScoped<MainService>();
    builder.Services.AddTransient<LogReqestMiddleware>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        logger.Info("ENV PROD");

        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors(policy =>
    {
        //string[]? urls = builder.Configuration.GetSection("Cors").Get<string[]>() ?? throw new Exception("appsettings Cors is null");
        string[] urls = settingsSection.Get<AppSettings>()?.Cors ?? throw new Exception("appsettings Cors is null");

        //policy.WithOrigins("https://sgart.sharepoint.com/", "https://sgart.sharepoint.com")
        policy.WithOrigins(urls)
            .AllowAnyMethod()
            .AllowAnyHeader();
    });

    app.UseMiddleware<LogReqestMiddleware>();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    // NLog: catch setup errors
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    logger.Info(C.LOG_STOP);
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}
