using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace WebMotors.API.Security
{
    /// <summary>
    /// Signing Configurations
    /// </summary>
    public class SigningConfigurations
    {
        /// <summary>
        /// Key
        /// </summary>
        public SecurityKey Key { get; }

        /// <summary>
        /// Signing Credentials
        /// </summary>
        public SigningCredentials SigningCredentials { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public SigningConfigurations()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(
                Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}
