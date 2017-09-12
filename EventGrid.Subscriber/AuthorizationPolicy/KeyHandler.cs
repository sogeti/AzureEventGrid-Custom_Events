using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Primitives;
using System.Linq;
using System.Threading.Tasks;

namespace EventGrid.Subscriber.AuthorizationPolicy
{
    public class KeyHandler : AuthorizationHandler<KeyRequirement>
    {
        private readonly IActionContextAccessor _accessor;

        public KeyHandler(IActionContextAccessor accessor)
        {
            _accessor = accessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, KeyRequirement requirement)
        {
            StringValues values;
            if (_accessor.ActionContext.HttpContext.Request.Query.TryGetValue("key", out values)
                && values.Count() == 1
                && values.Single().ToUpperInvariant() == requirement.Key.ToUpperInvariant())
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
