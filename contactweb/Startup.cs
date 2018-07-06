using ContactWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ContactWeb.Startup))]
namespace ContactWeb
{
    public partial class Startup
    {
        private const string ADMIN_ROLE = "Admin"; //match what we put in ELMAH!

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultRolesAndUsers();
        }

        private void CreateDefaultRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists(ADMIN_ROLE))
            {
                //Create the admin Role    
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = ADMIN_ROLE;
                roleManager.Create(role);

                //create a default admin : Only do this if you don't mind setting up this user
                //otherwise just write a SQL script to add your user to the Admin role

                //create the user here, only once
                var user = new ApplicationUser();
                user.UserName = "super.admin@awesomecontactweb.com";
                user.Email = "super.admin@awesomecontactweb.com";
                user.LockoutEnabled = true;
                
                string pwd = "62kqASrNFwKDwCA2"; //make it strong, then change it on first sign in.
                var createdUser = UserManager.Create(user, pwd);
                if (createdUser.Succeeded)
                {
                    var result = UserManager.AddToRole(user.Id, ADMIN_ROLE);
                }
            }

            /*
            //If you want, create this role and add all users to it
            if (!roleManager.RoleExists("ContactWebUser"))
            {
                //Create the ContactWebUser Role    
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "ContactWebUser";
                roleManager.Create(role);
            }
            */
        }
    }
}
