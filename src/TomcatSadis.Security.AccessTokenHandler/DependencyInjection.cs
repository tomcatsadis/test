using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TomcatSadis.Security.AccessTokenHandler.Settings;

namespace TomcatSadis.Security.AccessTokenHandler
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAccessTokenHandler(this IServiceCollection services)
        {
            #region Configuration Dependencies

            var configurationSection = services.BuildServiceProvider().GetService<IConfiguration>().GetSection(nameof(AccessTokenSettings));

            if (!configurationSection.Exists()) throw new ArgumentNullException($"'{nameof(AccessTokenSettings)}' configuration section is required.");

            services.AddSingleton<IAccessTokenSettings>(configurationSection.Get<AccessTokenSettings>());

            // Check if configuration is valid
            services.BuildServiceProvider().GetService<IAccessTokenSettings>().ValidateAttributes();

            #endregion

            #region Project Dependencies

            services.AddSingleton<IAccessTokenProvider, AccessTokenProvider>();

            #endregion

            return services;
        }
    }
}
