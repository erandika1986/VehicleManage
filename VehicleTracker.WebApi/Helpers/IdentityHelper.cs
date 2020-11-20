using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace VehicleTracker.WebApi.Helpers
{
    public static class IdentityHelper
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static string GetUsername()
        {
            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            var username = claim != null ? claim.Value : string.Empty;

            return username;
        }
    }
}
