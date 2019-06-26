using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Famer.Entity.Model;
using Famer.Entity.Concrete;

namespace Famer.WebUI.Models
{
	public class ProductsViewModel
	{
		public IEnumerable<SellerDetails> SD { get; set; }

		//精选产品
		public IEnumerable<CareChosen> CC { get; set; }
	}
}