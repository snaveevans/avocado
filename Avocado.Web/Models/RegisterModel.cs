using System.ComponentModel.DataAnnotations;

namespace Avocado.Web.Models
{
    public class RegisterModel : LoginModel
    {
        [Required]
        public string Name { get; set; }
        public string Picture { get; set; }
    }
}