namespace TomcatSadis.Security.WebJobs.AccessTokenHandler
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Azure.WebJobs.Host.Bindings;
    using Microsoft.Azure.WebJobs.Host.Protocols;
    using System.Threading.Tasks;
    using TomcatSadis.Security.WebJobs.AccessTokenHandler.Settings;

    /// <summary>
    /// Runs on every request and passes the function context (e.g. Http request and host configuration) to a value provider.
    /// </summary>
    public class AccessTokenBinding : IBinding
    {
        private readonly IAccessTokenSettings _accessTokenSettings;

        public AccessTokenBinding(IAccessTokenSettings accessTokenSettings)
        {
            _accessTokenSettings = accessTokenSettings;
        }

        public Task<IValueProvider> BindAsync(BindingContext context)
        {
            // Get the HTTP request
            var request = context.BindingData["req"] as HttpRequest;

            return Task.FromResult<IValueProvider>(new AccessTokenValueProvider(request, _accessTokenSettings));
        }

        public bool FromAttribute => true;

        public Task<IValueProvider> BindAsync(object value, ValueBindingContext context) => null;

        public ParameterDescriptor ToParameterDescriptor() => new ParameterDescriptor();
    }
}
