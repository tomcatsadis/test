using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace TomcatSadis.Security.WebJobs.AccessTokenHandler.Examples
{
    public static class ExtractUserIdController
    {
        [FunctionName("ExtractUserIdController")]
        public static  IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "extract-user-id")] HttpRequest req,
            [AccessToken] AccessTokenResult accessTokenResult,
            ILogger log)
        {
            if (accessTokenResult.Status != AccessTokenStatus.Valid)
            {
                return new UnauthorizedResult();
            }

            var userId = accessTokenResult.Principal.GetUserId();

            return new OkObjectResult(userId);
        }
    }
}
