using System;

namespace Avocado.Domain
{
    public class Event
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }

        [Obsolete("system constructor")]
        protected Event() { }

        public Event(string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException(nameof(description));

            Id = Guid.NewGuid();
            Title = title;
            Description = description;
        }
    }
}