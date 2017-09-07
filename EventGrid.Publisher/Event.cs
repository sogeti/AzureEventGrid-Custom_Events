using System;

namespace EventGrid.Publisher
{
    public class Event
    {
        public int Id { get; set; }

        public string EventType { get; set; }

        public string Subject { get; set; }

        public DateTime EventTime { get; set; }

        public dynamic Data { get; set; }
    }
}
