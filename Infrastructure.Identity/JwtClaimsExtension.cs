using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity
{
    public static class JwtClaimsExtension
    {
        public static long GetUserId(this ClaimsPrincipal user)
        {
            var idClaim = user?.FindFirst("Id");
            return idClaim != null && long.TryParse(idClaim.Value, out var id) ? id : 0;
        }

        public static List<string> GetRoles(this ClaimsPrincipal user)
        {
            return user?.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value)
                        .ToList() ?? new List<string>();
        }

        public static string GetEmail(this ClaimsPrincipal user)
        {
            return user?.FindFirst("Email")?.Value ?? string.Empty;
        }

        public static string GetFullName(this ClaimsPrincipal user)
        {
            return user?.FindFirst("FullName")?.Value ?? string.Empty;
        }

        public static string GetPhone(this ClaimsPrincipal user)
        {
            return user?.FindFirst("Phone")?.Value ?? string.Empty;
        }

        public static bool GetIsActive(this ClaimsPrincipal user)
        {
            var claim = user?.FindFirst("isActive");
            return claim != null && bool.TryParse(claim.Value, out var isActive) && isActive;
        }

        public static bool GetIsFirstLogin(this ClaimsPrincipal user)
        {
            var claim = user?.FindFirst("isFirstLogin");
            return claim != null && bool.TryParse(claim.Value, out var isFirst) && isFirst;
        }

        public static DateTime? GetCreatedAt(this ClaimsPrincipal user)
        {
            var claim = user?.FindFirst("createdAt");
            return claim != null && DateTime.TryParse(claim.Value, out var dt) ? dt : null;
        }

        public static DateTime? GetUpdatedAt(this ClaimsPrincipal user)
        {
            var claim = user?.FindFirst("updatedAt");
            return claim != null && DateTime.TryParse(claim.Value, out var dt) ? dt : null;
        }
    }
}
