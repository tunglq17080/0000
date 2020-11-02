using FPTApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FPTApp.Controllers
{
	public class RolesController : Controller
	{
		ApplicationDbContext context;
		public RolesController()
		{
			context = new ApplicationDbContext();
		}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
      {
        var Roles = context.Roles.ToList();
        return View(Roles);
      }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
      {
        var Role = new IdentityRole();
        return View(Role);
      }

      
      [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(IdentityRole Role)
      {
        context.Roles.Add(Role);
        context.SaveChanges();
        return RedirectToAction("Index");
      }


    }
  }