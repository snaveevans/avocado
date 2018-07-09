namespace Avocado.Web.Models
{
    public class ErrorModel
    {
        public string Key { get; set; }
        public string Message { get; set; }
        public ErrorModel() { }
        public ErrorModel(string key) : this(key, string.Empty) { }
        public ErrorModel(string key, string message)
        {
            Key = key;
            Message = message;
        }
    }
}