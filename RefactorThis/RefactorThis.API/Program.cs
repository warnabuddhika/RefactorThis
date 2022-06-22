using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Reflection;
using RefactorThis.Core.Common.Behaviours;
using RefactorThis.Core.Common.Interfaces;
using RefactorThis.Core.Projects.Commands;
using RefactorThis.Core.Projects.Queries;
using RefactorThis.Infrastructure.Persistence;
using RefactorThis.Core.Products.Commands;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Default");
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var configuration = new ConfigurationBuilder().AddJsonFile(string.Format("logsettings.{0}.json", environment)).Build();

Log.Logger = new LoggerConfiguration().ReadFrom
              .Configuration(configuration)
              .Enrich.WithClientIp()
              .Enrich.WithClientAgent()
              .Enrich.FromLogContext()
              .CreateLogger();
try
{
    Log.Information("Application starting up");
    builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(connectionString));
#pragma warning disable CS8603 // Possible null reference return.
    builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
#pragma warning restore CS8603 // Possible null reference return.
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
    builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    builder.Services.AddAuthentication("Bearer")
        .AddJwtBearer("Bearer", options =>
        {
            options.Authority = "https://localhost:5005";
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false
            };
        });
    builder.Services.AddAuthorization(optoins =>
    {
        optoins.AddPolicy("ClientIdPolicy", policy => policy.RequireClaim("client_id", "RefactorThis_portal_development"));
    });
    //builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
    //builder.Services.AddMediatR(typeof(Program).GetTypeInfo().Assembly);

    builder.Services.AddMediatR(typeof(CreateProductCommand).Assembly, typeof(GetProductsQuery).Assembly);

    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

    builder.Services.AddSwaggerGen();
   
    builder.Host.UseSerilog((ctx, lc) => lc    
       .ReadFrom.Configuration(configuration));


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseDeveloperExceptionPage();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    app.UseSerilogRequestLogging();
   // app.UseMiddleware<SerilogRequestLogger>();


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