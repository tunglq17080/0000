using FPTApp.Models;
using FPTApp.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;


namespace FPTApp.Controllers
{
	public class ManageUsersController : Controller
	{
		private ApplicationDbContext _context;
		public ManageUsersController()
		{
			_context = new ApplicationDbContext();
		}
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public ActionResult UsersWithRoles()
		{

			var usersWithRoles = (from user in _context.Users
														select new
														{
															UserId = user.Id,
															Name = user.Name,
															Username = user.UserName,
															Emailaddress = user.Email,
															RoleNames = (from userRole in user.Roles
																					 join role in _context.Roles
																					 on userRole.RoleId
																					 equals role.Id
																					 select role.Name).ToList()
														}).ToList().Select(p => new UsersInRole()
														{
															UserId = p.UserId,
															Name = p.Name,
															Username = p.Username,
															Email = p.Emailaddress,
															Role = string.Join(",", p.RoleNames)
														});


			return View(usersWithRoles);
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public ActionResult Edit(string id)
		{
			if (id == null)
			{
				return HttpNotFound();
			}
			var appUser = _context.Users.Find(id);
			if (appUser == null)
			{
				return HttpNotFound();
			}
			return View(appUser);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public ActionResult Edit(ApplicationUser user)
		{
			var userInDb = _context.Users.Find(user.Id);

			if (userInDb == null)
			{
				return View(user);
			}

			if (ModelState.IsValid)
			{
				userInDb.Name = user.Name;
				userInDb.UserName = user.UserName;
				userInDb.Phone = user.Phone;
				userInDb.Email = user.Email;
				userInDb.Age = user.Age;

				_context.Users.AddOrUpdate(userInDb);
				_context.SaveChanges();

				return RedirectToAction("UsersWithRoles");
			}
			return View(user);
		}

		[Authorize(Roles = "Admin")]
		public ActionResult Delete(string id)
		{
			var userInDb = _context.Users.SingleOrDefault(p => p.Id == id);

			if (userInDb == null)
			{
				return HttpNotFound();
			}
			_context.Users.Remove(userInDb);
			_context.SaveChanges();

			return RedirectToAction("UsersWithRoles");

		}
		[Authorize(Roles = "Admin")]
		[HttpGet]
		public ActionResult Details(string id)
		{
			var usersInDb = _context.Users.SingleOrDefault(p => p.Id == id);

			if (usersInDb == null)
			{
				return HttpNotFound();
			}

			return View(usersInDb);
		}
		
		
	}
}