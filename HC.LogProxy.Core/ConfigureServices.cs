using HC.LogProxy.Dal;
using Microsoft.Extensions.DependencyInjection;

namespace HC.LogProxy.Core
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddLogProxy(this IServiceCollection services)
        {
            return services.AddLogProxyDal()
                .AddTransient<ILoggingService, LoggingService>();
        }
    }
}