# TomcatSadis.Security.WebJobs.AccessTokenHandler

A library to handle JWT access token.

Use this library in your Azure Function HTTP Trigger to validate access token that exist in HTTP authorization request header.
Also you can extract information (e.g. user id, claims) from the access token.

This library use custom input binding, so you just need to add the binding to your WebJobs Startup, and use the binding in your API Service (HTTP Trigger).

## Technologies / Depedencies
- .Net Standard 2.1
- Depedency injection
- JWT

## How to use

1. Add the library to your project (from offline or NuGet Packages)

2. Inject the library in your Startup class as shown below.
``` csharp
...
using TomcatSadis.Security.WebJobs.AccessTokenHandler;
...
[assembly: WebJobsStartup(typeof(MyHttpService.WebJobsStartup))]
namespace MyHttpService
{
    public class WebJobsStartup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            ...
            builder.AddAccessTokenBinding();
            ...
        }
    }
}
```

3. Provide access token settings in your project as shown below. Change the settings value with your settings value.
``` json
{
  "AccessTokenSettings:PublicKey": "MIIBCgKCAQEA7oGXGFiW63LXxxi2So4Y19wbp8R7ij1iFn7EvFJnDHcvKvi+VLSdmvJjDf/eJDDRestuSN5MS6+35kvl74nSqXPDmTFjCWWQQlqPBX0S7K2XOtg7l/goBGwPMF47AM7gEbqR2WXDoVwy/ALwPeRZhCQrKM9aIx+FjRpZSnU4sYF7zUDpKeb6VjcpyPc9qxPGsIRqKBECHQIpzgq90/UAsqQRK3+QktXPhxOiDna372D5P5MIXx1lziXiZ2bdxJezPkgfUgLC0xO3BUuaxt+KPg9vRcYjVd/Rpo1K+zi9zMKvTCMMFZWBFNqvZXvmZ0oQvJiGhXcFdXCIWwjHPjlJkQIDAQAB",
  "AccessTokenSettings:Issuer": "TomcatSadis.AuthService",
  "AccessTokenSettings:Audience": "TomcatSadis.AccessTokenHandler"
}
```

4. To use the library to validate access token and extract information (e.g. user id and claims) in your API Service, add code as shown below.
``` csharp
...
using TomcatSadis.Security.WebJobs.AccessTokenHandler;
...
namespace MyHttpService
{
    public class MyController
    {
        [FunctionName("MyController")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "my-controller")] HttpRequest req,
            [AccessToken] AccessTokenResult accessTokenResult,
            ILogger log)
        {
            ...
            if (accessTokenResult.Status != AccessTokenStatus.Valid)
            {
                return new UnauthorizedResult();
            }

            var userId = accessTokenResult.Principal.GetUserId();
            ...
        }
    }
}
```

Source code and complete documentation can be found at
[This Repository](https://dev.azure.com/TomcatSadisLab/TomcatSadisPackage/_git/AccessTokenHandler)