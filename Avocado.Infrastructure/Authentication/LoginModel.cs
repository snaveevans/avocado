namespace Avocado.Infrastructure.Authentication
{
    public class LoginModel
    {
        public string Provider { get; set; }
        public string ProviderId { get; set; }
        public string ProviderKey { get; set; }
    }
}