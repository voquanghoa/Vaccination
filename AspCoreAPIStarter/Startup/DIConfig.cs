using AspBusiness.AutoConfig;
using AspCoreAPIStarter.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace AspCoreAPIStarter.Startup
{
    public static class DIConfig
    {
        public static void RegisterDI(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            AutoConfigScanner.Scan(services, typeof(AutoConfigScanner));
            services.AddSingleton<TokenProviderMiddleware>();
        }
    }
}