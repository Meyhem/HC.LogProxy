using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Authentication.Basic;
using HC.LogProxy.Configuration;
using Microsoft.Extensions.Options;

namespace HC.LogProxy.Api.Features.Authentication
{
    public class BasicAuthenticationService : IBasicUserValidationService
    {
        private readonly LogProxyOptions options;

        public BasicAuthenticationService(IOptions<LogProxyOptions> options)
        {
            this.options = options.Value;
        }

        public Task<bool> IsValidAsync(string username, string password)
        {
            if (options.Users is null) return Task.FromResult(false);

            return Task.FromResult(options.Users.Any(u => u.Username == username && u.Password == password));
        }
    }
}