using Microsoft.IdentityModel.Tokens;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace TomcatSadis.Security.AccessTokenHandler.Settings
{
    public class AccessTokenSettings : IAccessTokenSettings
    {
        private string _publicKey;

        [Required]
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

        [Required]
        public string Issuer { get; set; }

        [Required]
        public string Audience { get; set; }

        public SecurityKey RsaKey { get; private set; }

        public void ValidateAttributes()
        {
            Validator.ValidateObject(this, new ValidationContext(this), validateAllProperties: true);
        }
    }
}
