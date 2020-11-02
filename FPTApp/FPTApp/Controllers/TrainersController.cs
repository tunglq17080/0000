
using FPTApp.Models;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;

using System.Web.Mvc;

namespace FPTApp.Controllers
{
	public class TrainersController : Controller
	{
		private readonly ApplicationDbContext _context;
		public TrainersController()
		{
			_context = new ApplicationDbContext();
		}


		public ActionResult Index()
		{
			return View(_context.Trainers.ToList());
		}


		public ActionResult Create()
		{
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Trainer trainer)
		{
			if (ModelState.IsValid)
			{
				_context.Trainers.Add(trainer);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(trainer);
		}

		public ActionResult Edit(string id)
		{
			
			var trainer = _context.Users.FirstOrDefault(p => p.Id == id);
			if (trainer == null)
			{
				return HttpNotFound();
			}
			return View(trainer);
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

		public ActionResult Delete(int id)
		{
			var productInDb = _context.Trainers.SingleOrDefault(p => p.Id == id);

			if (productInDb == null)
			{
				return HttpNotFound();
			}
			_context.Trainers.Remove(productInDb);
			_context.SaveChanges();
			return RedirectToAction("Index");

		}

		public ActionResult Details(string id)
		{

			Trainer trainer = _context.Trainers.Find(id);
			if (trainer == null)
			{
				return HttpNotFound();
			}
			return View(trainer);
		}
	}
}