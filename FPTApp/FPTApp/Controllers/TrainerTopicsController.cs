using FPTApp.Models;
using FPTApp.ViewModel;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace FPTApp.Controllers
{
	public class TrainerTopicsController : Controller
	{
		private ApplicationDbContext _context;

		public TrainerTopicsController()
		{
			_context = new ApplicationDbContext();
		}
		[HttpGet]
		[Authorize(Roles = "TrainingStaff,Trainer")]
		public ActionResult Index()
		{
			if (User.IsInRole("TrainingStaff"))
			{
				var trainertopics = _context.TrainerTopics.Include(t => t.Topic).Include(t => t.Trainer).ToList();
				return View(trainertopics);
			}
			if (User.IsInRole("Trainer"))
			{
				var trainerId = User.Identity.GetUserId();
				var Res = _context.TrainerTopics.Where(e => e.TrainerId == trainerId).Include(t => t.Topic).ToList();
				return View(Res);
			}
			return View("Login");
		}
		[HttpGet]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Create()
		{

			var role = (from r in _context.Roles where r.Name.Contains("Trainer") select r).FirstOrDefault();
			var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();



			var topics = _context.Topics.ToList();

			var TrainerTopicVM = new TrainerTopicViewModel()
			{
				Topics = topics,
				Trainers = users,
				TrainerTopic = new TrainerTopic()
			};

			return View(TrainerTopicVM);
		}

		[HttpPost]
		public ActionResult Create(TrainerTopicViewModel model)
		{

			var role = (from r in _context.Roles where r.Name.Contains("Trainer") select r).FirstOrDefault();
			var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();


			var topics = _context.Topics.ToList();


			if (!ModelState.IsValid)
			{

				return View();

			}
			var trainerTopics = _context.TrainerTopics.ToList();
			var topicId = model.TrainerTopic.TopicId;

			var checkTrainerInCourse = trainerTopics
				.SingleOrDefault(c => c.TopicId == topicId && c.TrainerId == model.TrainerTopic.TrainerId);
			if (checkTrainerInCourse != null)
			{
				return RedirectToAction("Create");
			}
			var traineeCourse = new TrainerTopicViewModel()
			{
				Topics = topics,
				Trainers = users,
				TrainerTopic = new TrainerTopic()
			};
			_context.TrainerTopics.Add(model.TrainerTopic);
			_context.SaveChanges();
			return RedirectToAction("index");
		}
		[HttpGet]
		[Authorize(Roles = "TrainingStaff")]
		public ActionResult Delete(int id)
		{
			var trainerTopicInDb = _context.TrainerTopics
				.SingleOrDefault(p => p.Id == id);
			if (trainerTopicInDb == null)
			{
				return HttpNotFound();
			}
			_context.TrainerTopics.Remove(trainerTopicInDb);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}