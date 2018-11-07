using Avocado.Domain.Entities;

namespace Avocado.Web.Models
{
    public class TokenModel
    {
        public string Token { get; set; }
        public string Error { get; set; }
        public TokenModel(string token, string error = "")
        {
            Token = token;
            Error = error;
        }
    }
}
