using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<ApplicationUser> usermanager)
        {
            if (!usermanager.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    DisplayName = "marwa sharaf",
                    Email = "marwa@gmail.com",
                    UserName = "marwa.sharaf",
                    PhoneNumber = "01208466987"
                };
                await usermanager.CreateAsync(user,"P@ssw0rd");
            }
        }
    }
}
