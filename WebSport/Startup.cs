using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WebSport.Models;

[assembly: OwinStartupAttribute(typeof(WebSport.Startup))]
namespace WebSport
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            using (var context = new ApplicationDbContext())
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                if (!roleManager.Roles.Any())
                {
                    roleManager.Create(new IdentityRole{ Name = "Administrator" });
                    roleManager.Create(new IdentityRole { Name = "Competitor" });
                    roleManager.Create(new IdentityRole { Name = "Organizer" });
                    roleManager.Create(new IdentityRole { Name = "Visitor" });

                    var admin = new ApplicationUser();
                    admin.Email = admin.UserName = "admin@admin.admin";

                    if (userManager.Create(admin, "Pa$$w0rd").Succeeded)
                    {
                        userManager.AddToRole(admin.Id, "Administrator");
                        userManager.AddToRole(admin.Id, "Competitor");
                        userManager.AddToRole(admin.Id, "Organizer");
                        userManager.AddToRole(admin.Id, "Visitor");
                    }
                }
            }

        }
    }
}
