using System.ComponentModel.DataAnnotations;

namespace Avocado.Web.Models
{
    public class LoginModel
    {
        [Required]
        public string Provider { get; set; }
        [Required]
        public string AccessToken { get; set; }
    }
}