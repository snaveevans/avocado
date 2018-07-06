using System;
using Avocado.Domain;
using Avocado.Infrastructure.Enumerations;

namespace Avocado.Infrastructure.Authorization
{
    public class Login
    {
        public Guid Id { get; private set; }
        public Guid AccountId { get; private set; }
        public string Provider { get; private set; }
        public string ProviderId { get; private set; }  
        public string ProviderKey { get; private set; }
        
        [Obsolete("system constructor")]
        protected Login() { }
        internal Login(Account account, Providers provider, string providerId, string providerKey)
        {
            Id = Guid.NewGuid();
            AccountId = account.Id;
            Provider = provider.ToString();
            ProviderId = providerId;
            ProviderKey = providerKey;
        }
    }
}