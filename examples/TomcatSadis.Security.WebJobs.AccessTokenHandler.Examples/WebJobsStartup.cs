using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;

[assembly: WebJobsStartup(typeof(TomcatSadis.Security.WebJobs.AccessTokenHandler.Examples.WebJobsStartup))]
namespace TomcatSadis.Security.WebJobs.AccessTokenHandler.Examples
{
    public class WebJobsStartup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddAccessTokenBinding();
        }
    }
}
