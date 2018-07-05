namespace Avocado.Infrastructure.Authorization
{
    public class LoginOptions
    {
        public string Issuer { get; set; }
        public long MillisecondsUntilExpiration { get; set; }
        public string Key { get; set; }
    }
}