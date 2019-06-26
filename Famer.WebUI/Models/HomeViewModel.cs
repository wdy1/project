using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Famer.Entity.Model;

namespace Famer.WebUI.Models
{
	public class HomeViewModel
	{
		public IEnumerable<SellerDetails> Mor { get; set; }
		public IEnumerable<SellerDetails> Noon { get; set; }
		public IEnumerable<SellerDetails> Even { get; set; }
	}
}