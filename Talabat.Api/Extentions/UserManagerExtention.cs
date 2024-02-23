using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;

namespace Talabat.Api.Extentions
{
    public static class UserManagerExtention
    {
        public static async Task<ApplicationUser> FindUserWithAddressEmailAsync(this UserManager<ApplicationUser> userManager,ClaimsPrincipal currentuser)
        {
            var email = currentuser.FindFirstValue(ClaimTypes.Email);
            var user=userManager.Users.Include(u=>u.Address).FirstOrDefault(u => u.Email == email);
            return user;
        }

    }
}
