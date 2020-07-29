namespace Blog.Api.Helpers
{
    public class JwtConfigurations
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationInDays { get; set; }
    }
}