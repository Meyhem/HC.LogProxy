using Microsoft.Extensions.DependencyInjection;

namespace HC.LogProxy.Dal
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddLogProxyDal(this IServiceCollection services)
        {
            return services.AddTransient<ILogRepository, LogRepository>();
        }
    }
}