using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace EventGrid.Publisher
{
    public static class EventExtensions
    {
        public static string ToJson(this List<Event> value)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            };

            return JsonConvert.SerializeObject(value, settings);
        }
    }
}
