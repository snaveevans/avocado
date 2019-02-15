using Avocado.Domain.Entities;

namespace Avocado.Web.Models
{
    public class TokenModel
    {
        public string Token { get; set; }
        public TokenModel(string token)
        {
            Token = token;
        }
    }
}
