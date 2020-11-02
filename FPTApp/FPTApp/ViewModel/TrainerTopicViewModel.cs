using FPTApp.Models;
using System.Collections.Generic;

namespace FPTApp.ViewModel
{
	public class TrainerTopicViewModel
	{

		public Topic Topic { get; set; }
		public TrainerTopic TrainerTopic { get; set; }
		public IEnumerable<ApplicationUser> Trainers { get; set; }
		public IEnumerable<Topic> Topics { get; set; }
       
    }
}