namespace Avocado.Web.Models
{
    public class GoogleResponse
    {
        public string azp { get; set; }
        public string aud { get; set; }
        public string sub { get; set; }
        public string scope { get; set; }
        public string exp { get; set; }
        public string expires_in { get; set; }
        public string access_type { get; set; }

        public string ClientId => aud;
        public string GoogleId => sub;
    }
}