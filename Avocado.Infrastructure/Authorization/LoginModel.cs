namespace Avocado.Infrastructure.Authorization
{
    public class LoginModel
    {
        public string Provider { get; set; }
        public string ProviderId { get; set; }
        public string ProviderKey { get; set; }
    }
}