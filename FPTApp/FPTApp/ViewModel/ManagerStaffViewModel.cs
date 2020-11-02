using System.Collections.Generic;

namespace FPTApp.ViewModel
{
	public class ManagerStaffViewModel
	{

		public string UserId { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string RoleName { get; set; }
		public List<ManagerStaffViewModel> Trainee { get; set; }
		public List<ManagerStaffViewModel> Trainer { get; set; }


	}
}