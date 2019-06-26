using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Famer.Entity.Model;

namespace Famer.WebUI.Models
{
    public class YouXuanViewModel
    {
       public IEnumerable<Better100> xxgs{ get; set; }

       public IEnumerable<Better100> hxsc { get; set; }

       public IEnumerable<Better100> rqdp { get; set; }

       public IEnumerable<Better100> ldss { get; set; }

		public IEnumerable<Better100> benqiyouxuan { get; set; } 
    }
}