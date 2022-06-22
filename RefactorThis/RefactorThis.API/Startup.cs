using FluentValidation;
using MediatR;
using Serilog;
using System.Reflection;
using TimeSheet.Core.Common.Behaviours;
using TimeSheet.Core.Projects.Commands;
using TimeSheet.Core.Projects.Queries;

namespace TimeSheet.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
           services.AddRazorPages();
           // services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //services.AddEndpointsApiExplorer();
            //services.AddSwaggerGen();


            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


            //services.AddMediatR(typeof(CreateProjectCommand).Assembly, typeof(GetProjectsQuery).Assembly);

            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (!env.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/Error");
            //    app.UseHsts();
            //}


            //app.UseHttpsRedirection();
            //app.UseStaticFiles();
            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapRazorPages();
            //});
        }


    }
}
