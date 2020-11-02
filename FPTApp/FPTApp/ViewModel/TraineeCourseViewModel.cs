using FPTApp.Models;
using System.Collections.Generic;

namespace FPTApp.ViewModel
{
	public class TraineeCourseViewModel
	{
		public TraineeCourse TraineeCourse { get; set; }
		
		public IEnumerable<ApplicationUser> Trainees { get; set; }
		public IEnumerable<Course> Courses { get; set; }
      
    }
}