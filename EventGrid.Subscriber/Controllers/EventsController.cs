using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EventGrid.Subscriber.Controllers
{
    [Route("api/[controller]")]
    public class EventsController : Controller
    {
        private static List<Event> Events = new List<Event>();

        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(Events);
        }

        [HttpPost]
        public void Post([FromBody]List<Event> values)
        {
            Events.AddRange(values);
        }

        [HttpPost]
        [Validation] // Make sure this action is only fired for initial subscription validation requests
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Validate([FromBody]List<Event> values)
        {
            var validationEvent = values.SingleOrDefault();
            if (validationEvent != null)
            {
                // TODO: Validate that we are prepared to actually handle these events to avoid opening up to DDoS
                return new JsonResult(new SubscriptionValidationResponse { ValidationResponse = validationEvent.Data.validationCode });
            }
            return BadRequest();
        }
    }
}
