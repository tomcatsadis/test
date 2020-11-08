using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace TomcatSadis.Security.AccessTokenHandler.Examples
{
    public class ExtractUserIdController
    {
        private readonly IAccessTokenProvider _accessTokenProvider;

        public ExtractUserIdController(IAccessTokenProvider accessTokenProvider)
        {
            _accessTokenProvider = accessTokenProvider;
        }

        [FunctionName("ExtractUserIdController")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "extract-user-id")] HttpRequest req,
            ILogger log)
        {
            var accessTokenResult = _accessTokenProvider.ValidateToken(req);

            if (accessTokenResult.Status != AccessTokenStatus.Valid)
            {
                return new UnauthorizedResult();
            }

            var userId = accessTokenResult.Principal.GetUserId();

            return new OkObjectResult(userId);
        }
    }
}
