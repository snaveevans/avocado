using System.ComponentModel.DataAnnotations;

namespace Avocado.Web.Models
{
    public class ImpersonateModel
    {
        [Required]
        public string Provider { get; set; }
        [Required]
        public string ProviderKey { get; set; }
    }
}