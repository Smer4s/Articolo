using Web.Components;
using Web.Configurations;
using ApplicationDependencyInjection = Web.Configurations.DependencyInjection;

namespace Web
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(typeof(ApplicationDependencyInjection).Assembly);
            });

            builder.Services.AddWebServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.MapGet("/", (HttpResponse httpresponse) => httpresponse.Redirect("/feed"));

            app.Run();
        }
    }
}
