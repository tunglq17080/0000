using FPTApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;


[assembly: OwinStartupAttribute(typeof(FPTApp.Startup))]
namespace FPTApp
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
			createRolesandUsers();
		}


		private void createRolesandUsers()
		{
			ApplicationDbContext context = new ApplicationDbContext();

			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
			var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

  
			if (!roleManager.RoleExists("Admin"))
			{

				var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
				role.Name = "Admin";
				roleManager.Create(role);
			}

			var user = new ApplicationUser();
			user.UserName = "admin";
			user.Email = "admin@admin.com";

			string userPassword = "Bekhung1234@";

			var checkUser = UserManager.Create(user, userPassword);
			if (checkUser.Succeeded)
			{
				var result1 = UserManager.AddToRole(user.Id, "Admin");
			}


			// creating Creating TrainingStaff role     
			if (!roleManager.RoleExists("TrainingStaff"))
			{
				var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
				role.Name = "TrainingStaff";
				roleManager.Create(role);

			}

			// creating Creating Trainer role     
			if (!roleManager.RoleExists("Trainer"))
			{
				var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
				role.Name = "Trainer";
				roleManager.Create(role);

			}
			// creating Creating Trainee role     
			if (!roleManager.RoleExists("Trainee"))
			{
				var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
				role.Name = "Trainee";
				roleManager.Create(role);

			}
		}
	}
}