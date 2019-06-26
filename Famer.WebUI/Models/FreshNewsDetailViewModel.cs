using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Famer.Entity.Model;

namespace Famer.WebUI.Models
{
    public class FreshNewsDetailViewModel
    {
       public IEnumerable<NewsInfo> three { get; set; }
       public NewsInfo xiangqing { get; set; }
    }
}