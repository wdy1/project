using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Famer.WebUI.Models
{
	public class OrderViewModel
	{
		public int? CommitNum { get; set; }
		public string Image { get; set; }
		public string ProductName { get; set; }
		public decimal Price { get; set; }
		public string ShopId { get; set; }
		public int UserId { get; set; }
		public string UserName { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		public string ProductDes { get; set; }
		public string KindName { get; set; }
		public int ListId { get; set; }
		public int ProductId { get; set; }
	}
}