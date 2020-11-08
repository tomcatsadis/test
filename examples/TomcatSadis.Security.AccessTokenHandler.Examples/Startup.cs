using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(TomcatSadis.Security.AccessTokenHandler.Examples.Startup))]
namespace TomcatSadis.Security.AccessTokenHandler.Examples
{
    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddAccessTokenHandler();
        }
    }
}
