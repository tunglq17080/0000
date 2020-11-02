using FPTApp.Models;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FPTApp.Controllers
{
	public class TraineesController : Controller
	{
		private readonly ApplicationDbContext _context;
		public TraineesController()
		{
			_context = new ApplicationDbContext();
		}

		public ActionResult Index()
		{
			return View(_context.Trainees.ToList());
		}


		public ActionResult Details(int? id)
		{
			
			Trainee trainee = _context.Trainees.Find(id);
			if (trainee == null)
			{
				return HttpNotFound();
			}
			return View(trainee);
		}


		public ActionResult Create()
		{
			return View();
		}



		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Trainee trainee)
		{
			if (ModelState.IsValid)
			{
				_context.Trainees.Add(trainee);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(trainee);
		}


		public ActionResult Edit(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var trainee = _context.Users.FirstOrDefault(p => p.Id == id);
			if (trainee == null)
			{
				return HttpNotFound();
			}
			return View(trainee);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(ApplicationUser user)
		{
			var userInDb = _context.Users.Find(user.Id);

			if (userInDb == null)
			{
				return View(user);
			}

			if (ModelState.IsValid)
			{

				userInDb.Phone = user.Phone;
				userInDb.Email = user.Email;
				userInDb.UserName = user.UserName;


				_context.Users.AddOrUpdate(userInDb);
				_context.SaveChanges();

				return RedirectToAction("Index", "ManagerStaffViewModels");
			}
			return View(user);
		}


		

		[HttpGet]
		public ActionResult Delete(int id)
		{
			var traineeInDb = _context.Trainees.SingleOrDefault(p => p.Id == id);
			if (traineeInDb == null)
			{
				return HttpNotFound();
			}
			_context.Trainees.Remove(traineeInDb);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}