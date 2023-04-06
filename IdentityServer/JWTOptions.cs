namespace IdentityServer
{
    public class JWTOptions
    {
        public const string SectionName = "Jwt";
        public string Secret { get; set; }
        public double ExpiryTime { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
    }
}
