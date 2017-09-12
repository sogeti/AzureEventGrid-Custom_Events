using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Policy = "KeyRequired")]
        public IActionResult Post([FromBody]List<Event> values)
        {
            Events.AddRange(values);
            return Ok();
        }

        [HttpPost]
        [Validation] // Make sure this action is only fired for initial subscription validation requests
        [Authorize(Policy = "KeyRequired")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Validate([FromBody]List<Event> values)
        {
            var validationEvent = values.SingleOrDefault();
            if (validationEvent != null)
            {
                // TODO: We may want to check the topic: are we prepared to handle this?
                return new JsonResult(new SubscriptionValidationResponse { ValidationResponse = validationEvent.Data.validationCode });
            }
            return BadRequest();
        }
    }
}
