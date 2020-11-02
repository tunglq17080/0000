using FPTApp.Models;
using System.Collections.Generic;

namespace FPTApp.ViewModel
{
	public class CourseCategoryViewModel
	{
		public Course Course { get; set; }
		public IEnumerable<Category> Categories { get; set; }
		public IEnumerable<Topic> Topics { get; set; }
	}
}