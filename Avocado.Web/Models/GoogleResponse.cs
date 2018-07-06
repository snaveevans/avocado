namespace Avocado.Web.Models
{
    public class GoogleResponse
    {
        public string access_token { get; set; }
        public long expires_in { get; set; }
        public string token_type { get; set; }
    }
}