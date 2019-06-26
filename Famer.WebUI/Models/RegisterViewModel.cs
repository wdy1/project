using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Famer.Entity.Model;

namespace Famer.WebUI.Models
{
	public class RegisterViewModel
	{
		public string UserName { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public DateTime Time { get; set; }
	}
}