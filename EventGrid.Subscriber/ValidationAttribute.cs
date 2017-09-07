using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Linq;

namespace EventGrid.Subscriber
{
    public class ValidationAttribute : Attribute, IActionConstraint
    {
        public int Order => 0;

        public bool Accept(ActionConstraintContext context)
        {
            return context.RouteContext.HttpContext.Request.Headers.Any(header => header.Key == "aeg-event-type" && header.Value == "SubscriptionValidation");
        }
    }
}
