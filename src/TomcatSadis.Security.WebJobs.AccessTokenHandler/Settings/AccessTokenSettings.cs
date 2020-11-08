using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography;

namespace TomcatSadis.Security.WebJobs.AccessTokenHandler.Settings
{
    public class AccessTokenSettings : IAccessTokenSettings
    {
        private string _publicKey;

        public string PublicKey
        {
            get 
            {
                return _publicKey;
            }

            set
            {
                _publicKey = value;

                RSA rsa = RSA.Create();
                rsa.ImportRSAPublicKey(
                    source: Convert.FromBase64String(_publicKey),
                    bytesRead: out int _
                );

                RsaKey = new RsaSecurityKey(rsa);
            }
        }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public SecurityKey RsaKey { get; private set; }
    }
}
