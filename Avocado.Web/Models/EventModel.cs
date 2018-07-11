using System.Collections.Generic;

namespace Avocado.Web.Models
{
    public class EventModel
    {
        public string Title { get; set; }
        public string Description { get; set; }

        internal IEnumerable<ErrorModel> GetValidationErrors()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                yield return new ErrorModel(nameof(Title), "null");
            }
            if (string.IsNullOrWhiteSpace(Description))
            {
                yield return new ErrorModel(nameof(Description), "null");
            }
        }
    }
}