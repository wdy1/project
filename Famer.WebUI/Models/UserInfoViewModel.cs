using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Famer.WebUI.Models
{
	public class UserInfoViewModel
	{
		public int UserId { get; set; }
		public string UserName { get; set; }
		public string UserIntro { get; set; }
		public string Email { get; set; }
		public int? Year { get; set; }
		public int? Month { get; set; }
		public int? Day { get; set; }
		public string Sex { get; set; }
	}
}