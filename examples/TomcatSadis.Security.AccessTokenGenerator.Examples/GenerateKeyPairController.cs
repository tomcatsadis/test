using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace TomcatSadis.Security.AccessTokenGenerator.Examples
{
    public static class GenerateKeyPairController
    {
        /// <summary>
        /// API to generate key pair (private & public key).
        /// Private key is used to generate access token.
        /// Public key is used to validate the access token.
        /// 
        /// Generated key pair example:
        /// PrivateKey: MIIEpQIBAAKCAQEA7oGXGFiW63LXxxi2So4Y19wbp8R7ij1iFn7EvFJnDHcvKvi+VLSdmvJjDf/eJDDRestuSN5MS6+35kvl74nSqXPDmTFjCWWQQlqPBX0S7K2XOtg7l/goBGwPMF47AM7gEbqR2WXDoVwy/ALwPeRZhCQrKM9aIx+FjRpZSnU4sYF7zUDpKeb6VjcpyPc9qxPGsIRqKBECHQIpzgq90/UAsqQRK3+QktXPhxOiDna372D5P5MIXx1lziXiZ2bdxJezPkgfUgLC0xO3BUuaxt+KPg9vRcYjVd/Rpo1K+zi9zMKvTCMMFZWBFNqvZXvmZ0oQvJiGhXcFdXCIWwjHPjlJkQIDAQABAoIBAQCB3fRM4GgE+jp+AXm47NigKQyx9C2knznar9omBORxiDAZwOm6K8KpjRPcmpb1s9NMfpqleM2oZJzI/EjOfohDlnJJ5vdbNX8wcijwPyNf1kHDW9xPKmN3zPMUTirojLy7SpCCBIRaR17HlD4GJWGMrzkE9qrI9y/8Hf3CqkNdeuu7BmZ+mQ5uAcZw9M7bdCorADWuX1k+GA/mW1d1/F0vPbx4cN8qShFh6fjzDvteFVABMN0NzOZR240Qjfbdb7Ho+TAGAAsEd6niHTEXlIX/t4UQAmLMImF0a8uYWDJX+5aq5uQBH2S8yvXDBJVAuZXTIC/j1K3Qf2kj0XVYRz01AoGBAPdTeRGASCN6EIAlAIGrLYjr9icNlu7l3fX+0koyDGsSLXTLLGglzWKEt/NaqnVmPgf05s4PIpLFXHo1vY9fNAQ0go6idJ7LcQLjcysxrw6YK5ZNZW5MmkdiemowmXm0acTAZEkyqqPbvCgEvgcrv8i9Y2YVfNZp63bkMOictlY/AoGBAPbe7nLTlW8jt1l3k8PLtY/g/YKCWr4Hj+HlIo7tPiOUSjE2J4AgHrtVc18fSrX8CSMwigy14MDsCOV8LvW3gTsAxI+UYyPu0smkH2Qp4MbqJyliujPnELbukth/WvG1feDoZ/yV+YV6YAPlJ5zzbBqcHVaHQnaTE2wTbjJ7K4wvAoGAOkG6Ocoas+iTrGuK1ABLKH5UK9zCmaEhiEkupXVmgW31sRYObrXAzBzw62yGzEJ6CAvCtfTQsvu0DcFM1lGZgggQXKKdj63h/8ktnpYEYw6q7atrYfC/QmNK7GpoLEe3xjV/KdK6aQBgMJj1XeELOrCJkkkrb6Hhac7USmZneKcCgYEAuETSy1bvVdPdCaTd4OnvDgQsdfwC65ENbtnvn6uqFDid4HnBpjtTdRVlVn0u8QO9dkzG3pHrv1TvlwvIqZRdm8MI9PsXvTyIjgY5gDRaGV+x94w/3Hn+2ezeI0d8hKqp2PTgmYMAiwc7H+0uUlLIQFyC8ZFopMVHXAZs3LVfXfUCgYEAm8DuYo7lUjA9/kmntGCbEgLeMZNJsPqVn6HPfFlgFXD1ByUWoBGVZEgWDZxp0KoNXT6gIWYQPx3aUmDflJu8b2fQsoW/U11GUSQu05VE+O2E133UhGnalQBRlWGSxKOrjCf409cwXCEZaNJyrZ7yNn6tk/lTCMrokVbuJOIDswE=
        /// PublicKey: MIIBCgKCAQEA7oGXGFiW63LXxxi2So4Y19wbp8R7ij1iFn7EvFJnDHcvKvi+VLSdmvJjDf/eJDDRestuSN5MS6+35kvl74nSqXPDmTFjCWWQQlqPBX0S7K2XOtg7l/goBGwPMF47AM7gEbqR2WXDoVwy/ALwPeRZhCQrKM9aIx+FjRpZSnU4sYF7zUDpKeb6VjcpyPc9qxPGsIRqKBECHQIpzgq90/UAsqQRK3+QktXPhxOiDna372D5P5MIXx1lziXiZ2bdxJezPkgfUgLC0xO3BUuaxt+KPg9vRcYjVd/Rpo1K+zi9zMKvTCMMFZWBFNqvZXvmZ0oQvJiGhXcFdXCIWwjHPjlJkQIDAQAB
        /// </summary>
        [FunctionName("GenerateKeyPair")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "generate-key-pair")] HttpRequest req,
            ILogger log)
        {
            var keyPair = RSAKeyPairGenerator.GenerateRSAKeyPair();

            return new OkObjectResult(keyPair);
        }
    }
}
