using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var configuration = new ConfigurationBuilder().AddJsonFile(string.Format("logsettings.{0}.json", environment)).Build();
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

try
{
    Log.Information("Application starting up");
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Configuration.AddJsonFile("ocelot.json");

    var identityUrl = builder.Configuration.GetValue<string>("IdentityServer:Url");
    var requireHTTPS = builder.Configuration.GetValue<bool>("IdentityServer:RequireHTTPS");
    var identityApiKey = builder.Configuration.GetValue<string>("IdentityServer:IdentityApiKey");
    builder.Services.AddAuthentication()
        .AddJwtBearer(identityApiKey, options =>
        {
            options.Authority = identityUrl;
            options.RequireHttpsMetadata = requireHTTPS;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false
            };
        });
    builder.Services.AddOcelot();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseEndpoints(endpoints => endpoints.MapControllers());
    app.UseOcelot();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "The application failed to start correctly.");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}

