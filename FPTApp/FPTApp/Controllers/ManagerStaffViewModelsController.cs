using FPTApp.Models;
using FPTApp.ViewModel;
using System.Linq;
using System.Web.Mvc;

namespace FPTApp.Controllers
{
	public class ManagerStaffViewModelsController : Controller
	{
		ApplicationDbContext _context;
		public ManagerStaffViewModelsController()
		{
			_context = new ApplicationDbContext();
		}
		[HttpGet]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Index()


		{

			var role = (from r in _context.Roles where r.Name.Contains("Trainee") select r).FirstOrDefault();
			var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();
			var userVM = users.Select(user => new ManagerStaffViewModel
			{
				UserName = user.UserName,
				Email = user.Email,
				RoleName = "Trainee",
				UserId = user.Id
			}).ToList();

			var role2 = (from r in _context.Roles where r.Name.Contains("Trainer") select r).FirstOrDefault();
			var admins = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role2.Id)).ToList();
			var adminVM = admins.Select(user => new ManagerStaffViewModel
			{
				UserName = user.UserName,
				Email = user.Email,
				RoleName = "Trainer",
				UserId = user.Id
			}).ToList();


			var model = new ManagerStaffViewModel { Trainee = userVM, Trainer = adminVM };
			return View(model);
		}



	}
}
