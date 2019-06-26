using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Famer.WebUI.Models
{
	public class CartFormViewModel
	{
		public int ProductId { get; set; }
		public string ProductDes { get; set; }
		public string Image { get; set; }
		public decimal Price { get; set; }
		public int ListID { get; set; }
	}
}