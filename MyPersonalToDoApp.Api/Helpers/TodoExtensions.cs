using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.Api.Helpers
{
    public static class TodoExtensions
    {
        public static string ToGuidString(this Guid guid)
        {
            return guid.ToString().ToLower();
        }
    }
}
