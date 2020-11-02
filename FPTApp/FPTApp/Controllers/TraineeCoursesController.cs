using FPTApp.Models;
using FPTApp.ViewModel;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace FPTApp.Controllers
{
	public class TraineeCoursesController : Controller
	{
		private ApplicationDbContext _context;

		public TraineeCoursesController()
		{
			_context = new ApplicationDbContext();
		}
		[HttpGet]
		[Authorize(Roles = "TrainingStaff,Trainee")]
		public ActionResult Index()
		{
			if (User.IsInRole("TrainingStaff"))
			{
				var traineecourses = _context.TraineeCourses
					.Include(t => t.Course)
					.Include(t => t.Trainee).ToList();
				return View(traineecourses);
			}
			if (User.IsInRole("Trainee"))
			{
				var traineeId = User.Identity.GetUserId();
				var Res = _context.TraineeCourses
					.Where(e => e.TraineeId == traineeId)
					.Include(t => t.Course).ToList();
				return View(Res);
			}
			return View("Login");
		}
		[HttpGet]

		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Create()
		{
			//get trainer
			var role = (from r in _context.Roles where r.Name.Contains("Trainee") select r).FirstOrDefault();
			var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

			//get topic

			var courses = _context.Courses.ToList();

			var TraineeTopicVM = new TraineeCourseViewModel()
			{
				Courses = courses,
				Trainees = users,
				TraineeCourse = new TraineeCourse()
			};

			return View(TraineeTopicVM);
		}

		[HttpPost]
		public ActionResult Create(TraineeCourseViewModel model)
		{
			var role = (from r in _context.Roles where r.Name.Contains("Trainee") select r).FirstOrDefault();
			var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

			var courses = _context.Courses.ToList();

			if (!ModelState.IsValid)
			{

				return View();

			}
			var traineeeCourses = _context.TraineeCourses.ToList();
			var courseId = model.TraineeCourse.CourseId;

			var checkTraineeInCourse = traineeeCourses.SingleOrDefault(c => c.CourseId == courseId && c.TraineeId == model.TraineeCourse.TraineeId);
			if (checkTraineeInCourse != null)
			{
				return RedirectToAction("Create");
			}
			var traineeCourse = new TraineeCourseViewModel()
			{
				Courses = courses,
				Trainees = users,
				TraineeCourse = new TraineeCourse()
			};
			_context.TraineeCourses.Add(model.TraineeCourse);
			_context.SaveChanges();
			return RedirectToAction("index");

		}
		[HttpGet]

		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Delete(int id)
		{
			var traineeCourseInDb = _context.TraineeCourses
				.SingleOrDefault(p => p.Id == id);
			if (traineeCourseInDb == null)
			{
				return HttpNotFound();
			}
			_context.TraineeCourses.Remove(traineeCourseInDb);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}