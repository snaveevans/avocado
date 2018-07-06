using Avocado.Infrastructure.Authorization;

namespace Avocado.Infrastructure.Authorization
{
    public class RegisterModel : LoginModel
    {
        public string Name { get; set; }
        public string Picture { get; set; }
    }
}