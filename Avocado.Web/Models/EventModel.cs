using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Avocado.Web.Models
{
    /// <summary>
    /// Structure for creating and updating events.
    /// </summary>
    public class EventModel
    {
        /// <summary>
        /// The title the event will be set to.
        /// </summary>
        [Required]
        public string Title { get; set; }
        /// <summary>
        /// The description the description will be set to.
        /// </summary>
        [Required]
        public string Description { get; set; }
    }
}
