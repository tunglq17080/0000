using FPTApp.Models;
using System.Linq;
using System.Web.Mvc;

namespace FPTApp.Controllers
{
	public class TopicsController : Controller
	{
		private ApplicationDbContext _context = new ApplicationDbContext();


		[HttpGet]

		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Index()
		{
			return View(_context.Topics.ToList());
		}




		public ActionResult Create()
		{
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Topic topic)
		{
			if (_context.Topics.Any(p => p.Name.Contains(topic.Name)))
			{
				ModelState.AddModelError("Name", "Courses Name Already Exists.");
				return View();
			}
			if (ModelState.IsValid)
			{
				_context.Topics.Add(topic);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(topic);
		}




		[HttpGet]

		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Edit(int id)
		{
			var topicInDb = _context.Topics
				.SingleOrDefault(p => p.Id == id);
			if (topicInDb == null)
			{
				return HttpNotFound();
			}
			return View(topicInDb);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Topic topic)
		{
			var topicInDb = _context.Topics
				.SingleOrDefault(p => p.Id == topic.Id);
			if (topicInDb == null)
			{
				return HttpNotFound();
			}

			topicInDb.Name = topic.Name;
			_context.SaveChanges();
			return RedirectToAction("Index");
		}


		[HttpGet]

		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Delete(int id)
		{
			var topicInDb = _context.Topics
				.SingleOrDefault(p => p.Id == id);
			if (topicInDb == null)
			{
				return HttpNotFound();
			}
			_context.Topics.Remove(topicInDb);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}

}