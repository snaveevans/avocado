using System;

namespace Avocado.Domain.Entities
{
    /// <summary>
    /// Avocado event item
    /// </summary>
    public class Event
    {
        /// <summary>
        /// App unique id of event.
        /// </summary>
        public Guid Id { get; private set; }
        /// <summary>
        /// String by which the event item is known by.
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// Additional text about the event
        /// </summary>
        public string Description { get; private set; }

        internal Event(string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException(nameof(description));

            Id = Guid.NewGuid();
            Title = title;
            Description = description;
        }

        internal void UpdateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title));
            Title = title;
        }

        internal void UpdateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException(nameof(description));
            Description = description;
        }
    }
}