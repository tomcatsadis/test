namespace TomcatSadis.Security.WebJobs.AccessTokenHandler
{
    using Microsoft.Azure.WebJobs.Host.Bindings;
    using System;
    using System.Threading.Tasks;
    using TomcatSadis.Security.WebJobs.AccessTokenHandler.Settings;

    /// <summary>
    /// Provides a new binding instance for the function host.
    /// </summary>
    public class AccessTokenBindingProvider : IBindingProvider
    {
        public Task<IBinding> TryCreateAsync(BindingProviderContext context)
        {
            IAccessTokenSettings _accessTokenSettings = new AccessTokenSettings
            {
                PublicKey = Environment.GetEnvironmentVariable($"{nameof(AccessTokenSettings)}:{nameof(IAccessTokenSettings.PublicKey)}"),
                Audience = Environment.GetEnvironmentVariable($"{nameof(AccessTokenSettings)}:{nameof(IAccessTokenSettings.Audience)}"),
                Issuer = Environment.GetEnvironmentVariable($"{nameof(AccessTokenSettings)}:{nameof(IAccessTokenSettings.Issuer)}"),
            };
            
            if (string.IsNullOrWhiteSpace(_accessTokenSettings.PublicKey))
            {
                throw new ArgumentNullException($"{nameof(AccessTokenSettings)}:{nameof(IAccessTokenSettings.PublicKey)} setting is not exist or empty.");
            }

            if (string.IsNullOrWhiteSpace(_accessTokenSettings.Audience))
            {
                throw new ArgumentNullException($"{nameof(AccessTokenSettings)}:{nameof(IAccessTokenSettings.Audience)} setting is not exist or empty.");
            }

            if (string.IsNullOrWhiteSpace(_accessTokenSettings.Issuer))
            {
                throw new ArgumentNullException($"{nameof(AccessTokenSettings)}:{nameof(IAccessTokenSettings.Issuer)} setting is not exist or empty.");
            }

            IBinding binding = new AccessTokenBinding(_accessTokenSettings);
            return Task.FromResult(binding);
        }
    }

}
