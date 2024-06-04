using WebAPI.Configurations;
using WebAPI.Middleware;
using ApplicationDependencyInjection = WebAPI.Configurations.DependencyInjection;

namespace WebAPI;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddMediatR(config =>
		{
			config.RegisterServicesFromAssemblies(typeof(ApplicationDependencyInjection).Assembly);
		});

		builder.Services.AddSwagger();

		builder.Services.AddControllers();

		builder.Services.AddWebServices();
		builder.Services.AddInfrastructureServices(builder.Configuration);
		builder.Services.ConfigureAutoMapper();
		builder.Services.ConfigureAuthorization();

		var app = builder.Build();

		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Error");
			app.UseHsts();
		}

		app.UseMiddleware<ExceptionMiddleware>();

		app.UseSwagger();
		app.UseSwaggerUI();

		app.UseRouting();
		app.UseAuthentication();
		app.UseAuthorization();
		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers();
		});

//		app.UseHttpsRedirection();

		app.MapGet("/", (HttpResponse httpresponse) => httpresponse.Redirect("/swagger/index.html"))
			.ExcludeFromDescription();

		app.Run();
	}
}
