using System;
using System.Security.Cryptography;

namespace TomcatSadis.Security.AccessTokenGenerator
{
    public static class RSAKeyPairGenerator
    {
        /// <summary>
        /// Program to generate RSA key pair (private and public key)
        /// </summary>
        /// <returns>
        /// <code>KeyPair</code> Object with PrivateKey and PublicKey properties
        /// </returns>
        public static KeyPair GenerateRSAKeyPair()
        {
            using RSA rsa = RSA.Create();

            return new KeyPair
            {
                PrivateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey()),
                PublicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey())
            };
        }

        public class KeyPair
        {
            public string PrivateKey { get; set; }

            public string PublicKey { get; set; }
        }
    }
}
