namespace Web.Configurations
{
    public static class DependencyInjection
    {
        public static void AddWebServices(this IServiceCollection services)
        {
            services.AddRazorComponents()
                .AddInteractiveServerComponents();
        }
    }
}
