using System;

namespace EventGrid.Subscriber
{
    public class Event
    {
        public string Id { get; set; }

        public string EventType { get; set; }

        public string Subject { get; set; }

        public DateTime EventTime { get; set; }

        public dynamic Data { get; set; }

        public string Topic { get; set; }
    }
}
