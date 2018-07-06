using Avocado.Infrastructure.Authorization;

namespace Avocado.Web.Models
{
    public class RegisterModel : LoginModel
    {
        public string Name { get; set; }
    }
}