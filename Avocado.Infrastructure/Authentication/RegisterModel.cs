using Avocado.Infrastructure.Authentication;

namespace Avocado.Infrastructure.Authentication
{
    public class RegisterModel : LoginModel
    {
        public string Name { get; set; }
        public string Picture { get; set; }
    }
}