using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventGrid.Subscriber.AuthorizationPolicy
{
    public class KeyRequirement: IAuthorizationRequirement
    {
        public string Key { get; private set; }

        public KeyRequirement(string key)
        {
            Key = key;
        }
    }
}
