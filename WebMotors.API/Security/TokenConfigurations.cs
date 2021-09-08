namespace WebMotors.API.Security
{
    /// <summary>
    /// Token Configurations
    /// </summary>
    public class TokenConfigurations
    {
        /// <summary>
        /// Audience
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Issuer
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Expiration Type
        /// </summary>
        public string ExpirationType { get; set; }

        /// <summary>
        /// Seconds
        /// </summary>
        public int Seconds { get; set; }

        /// <summary>
        /// Minutes
        /// </summary>
        public int Minutes { get; set; }

        /// <summary>
        /// Days
        /// </summary>
        public int Days { get; set; }

        /// <summary>
        /// Secret JWT
        /// </summary>
        private string secretJWT;

        /// <summary>
        /// Secret JWT
        /// </summary>
        public string SecretJWT
        {
            get { return secretJWT; }
            set { secretJWT = value; }
        }
    }
}
