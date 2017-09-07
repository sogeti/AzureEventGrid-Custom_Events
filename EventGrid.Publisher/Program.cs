using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace EventGrid.Publisher
{
    class Program
    {
        /* See https://docs.microsoft.com/en-us/azure/event-grid/custom-event-quickstart
        for setting up the custom publisher and subscriber, and for retrieving below values */

        private const string EventKey = "<your SAS key here>";
        private const string EventEndpoint = "<your event endpoint here>";

        static void Main(string[] args)
        {
            var content = new StringContent(GenerateEvents(), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(EventEndpoint));
            request.Headers.Add("aeg-sas-key", EventKey);
            request.Content = content;

            var client = new HttpClient();
            var response = client.SendAsync(request).GetAwaiter().GetResult();

            if (!response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                throw new Exception(responseContent);
            }
        }

        private static string GenerateEvents()
        {
            var random = new Random();
            var event1 = new Event()
            {
                Id = random.Next(),
                EventTime = DateTime.Now,
                Subject = "publisherApp/v1/resource",
                EventType = "somethingRandomHappened",
                Data = new
                {
                    Id = random.Next(),
                    Url = "http://pointer.to/resource"
                }
            };
            var event2 = new Event()
            {
                Id = random.Next(),
                EventTime = DateTime.Now,
                Subject = "publisherApp/v2/resource",
                EventType = "somethingRandomHappened",
                Data = new
                {
                    Id = random.Next(),
                    Url = "http://pointer.to/resource"
                }
            };
            var list = new List<Event>() { event1, event2 };
            return list.ToJson();
        }
    }
}
