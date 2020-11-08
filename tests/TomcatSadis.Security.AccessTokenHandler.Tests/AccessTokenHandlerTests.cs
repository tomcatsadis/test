﻿using Microsoft.AspNetCore.Http;
using TomcatSadis.Security.AccessTokenHandler.Settings;
using Xunit;

namespace TomcatSadis.Security.AccessTokenHandler.Tests
{
    /*
     * Before we run this test, we manually generate the access token. 
     * We use https://jwt.io/ to generate it.
     * 
     * For example, the JWT payload data is:
     * {
     *   "sub": "test123",
     *   "given_name": "Albert",
     *   "family_name": "Brucelee",
     *   "nbf": 1604780115,
     *   "exp": 1635884115,
     *   "iss": "TomcatSadis.AuthService",
     *   "aud": "TomcatSadis.AccessTokenHandler"
     * }
     * 
     * And the RSA key pair is:
     * Private Key: MIIEpQIBAAKCAQEA7oGXGFiW63LXxxi2So4Y19wbp8R7ij1iFn7EvFJnDHcvKvi+VLSdmvJjDf/eJDDRestuSN5MS6+35kvl74nSqXPDmTFjCWWQQlqPBX0S7K2XOtg7l/goBGwPMF47AM7gEbqR2WXDoVwy/ALwPeRZhCQrKM9aIx+FjRpZSnU4sYF7zUDpKeb6VjcpyPc9qxPGsIRqKBECHQIpzgq90/UAsqQRK3+QktXPhxOiDna372D5P5MIXx1lziXiZ2bdxJezPkgfUgLC0xO3BUuaxt+KPg9vRcYjVd/Rpo1K+zi9zMKvTCMMFZWBFNqvZXvmZ0oQvJiGhXcFdXCIWwjHPjlJkQIDAQABAoIBAQCB3fRM4GgE+jp+AXm47NigKQyx9C2knznar9omBORxiDAZwOm6K8KpjRPcmpb1s9NMfpqleM2oZJzI/EjOfohDlnJJ5vdbNX8wcijwPyNf1kHDW9xPKmN3zPMUTirojLy7SpCCBIRaR17HlD4GJWGMrzkE9qrI9y/8Hf3CqkNdeuu7BmZ+mQ5uAcZw9M7bdCorADWuX1k+GA/mW1d1/F0vPbx4cN8qShFh6fjzDvteFVABMN0NzOZR240Qjfbdb7Ho+TAGAAsEd6niHTEXlIX/t4UQAmLMImF0a8uYWDJX+5aq5uQBH2S8yvXDBJVAuZXTIC/j1K3Qf2kj0XVYRz01AoGBAPdTeRGASCN6EIAlAIGrLYjr9icNlu7l3fX+0koyDGsSLXTLLGglzWKEt/NaqnVmPgf05s4PIpLFXHo1vY9fNAQ0go6idJ7LcQLjcysxrw6YK5ZNZW5MmkdiemowmXm0acTAZEkyqqPbvCgEvgcrv8i9Y2YVfNZp63bkMOictlY/AoGBAPbe7nLTlW8jt1l3k8PLtY/g/YKCWr4Hj+HlIo7tPiOUSjE2J4AgHrtVc18fSrX8CSMwigy14MDsCOV8LvW3gTsAxI+UYyPu0smkH2Qp4MbqJyliujPnELbukth/WvG1feDoZ/yV+YV6YAPlJ5zzbBqcHVaHQnaTE2wTbjJ7K4wvAoGAOkG6Ocoas+iTrGuK1ABLKH5UK9zCmaEhiEkupXVmgW31sRYObrXAzBzw62yGzEJ6CAvCtfTQsvu0DcFM1lGZgggQXKKdj63h/8ktnpYEYw6q7atrYfC/QmNK7GpoLEe3xjV/KdK6aQBgMJj1XeELOrCJkkkrb6Hhac7USmZneKcCgYEAuETSy1bvVdPdCaTd4OnvDgQsdfwC65ENbtnvn6uqFDid4HnBpjtTdRVlVn0u8QO9dkzG3pHrv1TvlwvIqZRdm8MI9PsXvTyIjgY5gDRaGV+x94w/3Hn+2ezeI0d8hKqp2PTgmYMAiwc7H+0uUlLIQFyC8ZFopMVHXAZs3LVfXfUCgYEAm8DuYo7lUjA9/kmntGCbEgLeMZNJsPqVn6HPfFlgFXD1ByUWoBGVZEgWDZxp0KoNXT6gIWYQPx3aUmDflJu8b2fQsoW/U11GUSQu05VE+O2E133UhGnalQBRlWGSxKOrjCf409cwXCEZaNJyrZ7yNn6tk/lTCMrokVbuJOIDswE=
     * Public Key: MIIBCgKCAQEA7oGXGFiW63LXxxi2So4Y19wbp8R7ij1iFn7EvFJnDHcvKvi+VLSdmvJjDf/eJDDRestuSN5MS6+35kvl74nSqXPDmTFjCWWQQlqPBX0S7K2XOtg7l/goBGwPMF47AM7gEbqR2WXDoVwy/ALwPeRZhCQrKM9aIx+FjRpZSnU4sYF7zUDpKeb6VjcpyPc9qxPGsIRqKBECHQIpzgq90/UAsqQRK3+QktXPhxOiDna372D5P5MIXx1lziXiZ2bdxJezPkgfUgLC0xO3BUuaxt+KPg9vRcYjVd/Rpo1K+zi9zMKvTCMMFZWBFNqvZXvmZ0oQvJiGhXcFdXCIWwjHPjlJkQIDAQAB
     * 
     * The generated access token is:
     * eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjVmYTY2NTA1ODgxNjFjMDExZDljNjdkYSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJBbGJlcnQgQnJ1Y2VsZWUiLCJuYmYiOjE2MDQ3NDA0NDYsImV4cCI6MTYzNTg0NDQ0NiwiaXNzIjoiU2ltcGxlQ2hhdC5BdXRoU2VydmljZSIsImF1ZCI6IlNpbXBsZUNoYXQuQWNjZXNzVG9rZW5IYW5kbGVyIn0.vrDlewPBqdOTp01NlW4tpAns8_phN2KExYEThaSjFw9OmGJVlEMWUQL_p6PHID0YFLzTQxRehNBaulEbgQaO2K7svaXMkwXRcVUgGudgDmJp-jooUpiMrbV9j1yxb9pMbJdtNco-Xp5yp2_ryce-kju2qQ_BS_drRWc8bEF83VXHSOQcZTW36-02uwzL23OLrjBlKSu44qV5VaFR63pWQ4IlUcOE2kcfujtX0AOdOaDoRYYJ_weNhaW6HNwLvDxCAkKcbqWYFVCjFWHsgoIQgdk3uHejl7LD0ay_ymSHhZxzx_PgKFfAYQDALji9_pXsbRMxCaSN-ZpKxBHQzLQjMA
     * 
     * So in this tests, we test our program to decode access token,
     * and check if the program can validate the token signature using provided public key.
     * Also we check if the decoded JWT payload data match with expected, e.g. the "sub" (user id) value
     * 
     * */

    public class AccessTokenHandlerTests
    {
        [Theory]
        [InlineData(
            "MIIBCgKCAQEA7oGXGFiW63LXxxi2So4Y19wbp8R7ij1iFn7EvFJnDHcvKvi+VLSdmvJjDf/eJDDRestuSN5MS6+35kvl74nSqXPDmTFjCWWQQlqPBX0S7K2XOtg7l/goBGwPMF47AM7gEbqR2WXDoVwy/ALwPeRZhCQrKM9aIx+FjRpZSnU4sYF7zUDpKeb6VjcpyPc9qxPGsIRqKBECHQIpzgq90/UAsqQRK3+QktXPhxOiDna372D5P5MIXx1lziXiZ2bdxJezPkgfUgLC0xO3BUuaxt+KPg9vRcYjVd/Rpo1K+zi9zMKvTCMMFZWBFNqvZXvmZ0oQvJiGhXcFdXCIWwjHPjlJkQIDAQAB",
            "TomcatSadis.AuthService",
            "TomcatSadis.AccessTokenHandler",
            "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ0ZXN0MTIzIiwiZ2l2ZW5fbmFtZSI6IkFsYmVydCIsImZhbWlseV9uYW1lIjoiQnJ1Y2VsZWUiLCJuYmYiOjE2MDQ3MTA5MjMsImV4cCI6MTYzNTgxNDkyMywiaXNzIjoiVG9tY2F0U2FkaXMuQXV0aFNlcnZpY2UiLCJhdWQiOiJUb21jYXRTYWRpcy5BY2Nlc3NUb2tlbkhhbmRsZXIifQ.c7wf1jkZZQuqo6eyWDWKsJiluoeYCTdij3gzNm8kVaXm3hmZhgOTpfaXWm-fUMlYxQJLG1jDtYvEqIRlYAPnfQofpHiwWh72OfnIvuhPFZVIy_BiUpbz888BaZZTwqjvEI1XBBfu7UGTfIp8xb6xwcwOaHoO-VCxhOzw5vYe_tUS5a_G8k-_uINYtWJuselqpgYNMbOotb41_jMUgpr13-zhy3gPOwolDw-AxJt4RxNuDFdkfjHAfr1TxhGcZ8B9l5X7aQv2_myPqIRK9Hbp-ShShZ3hIeShNq1DToUECgEMdLK6CufdQCr6oMUeO-2xOl2zSOqN9GhWvdgXJvs3CQ",
            "test123",
            "Albert",
            "Brucelee")]
        public void DecodedAccessTokenShouldBeValid(
            string publicKey,
            string issuer,
            string audience,
            string accessToken,
            string expectedUserId,
            string expectedGivenName,
            string expectedFamilyName)
        {
            var accessTokenSettings = new AccessTokenSettings
            {
                PublicKey = publicKey,
                Issuer = issuer,
                Audience = audience
            };

            var AccessTokenProvider = new AccessTokenProvider(accessTokenSettings);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = $"Bearer {accessToken}";

            var accessTokenResult = AccessTokenProvider.ValidateToken(httpContext.Request);

            Assert.Equal(AccessTokenStatus.Valid, accessTokenResult.Status);
            Assert.Equal(expectedUserId, accessTokenResult.Principal.GetUserId());
            Assert.Equal(expectedGivenName, accessTokenResult.Principal.GetGivenName());
            Assert.Equal(expectedFamilyName, accessTokenResult.Principal.GetFamilyName());
        }
    }
}
