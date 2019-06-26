using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Famer.Entity.Model;

namespace Famer.WebUI.Models
{
    public class FreshNewsViewModel
    {
        public NewsInfo newsinfo { get; set; }
        public IEnumerable<NewsInfo> tjdd { get; set; }
        public IEnumerable<NewsInfo> tjdd1 { get; set; }
        public IEnumerable<NewsInfo> jszq { get; set; }
        public IEnumerable<NewsInfo> jszq1 { get; set; }
        public IEnumerable<NewsInfo> xxzx { get; set; }
        public IEnumerable<NewsInfo> ngwh { get; set; }
        public IEnumerable<NewsInfo> ngwh1 { get; set; }
        public IEnumerable<NewsInfo> nyzh { get; set; }
        public IEnumerable<NewsInfo> nyzh1 { get; set; }
        public IEnumerable<NewsInfo> wdlj { get; set; }
        public IEnumerable<NewsInfo> gjts { get; set; }
        public IEnumerable<NewsInfo> lb { get; set; }
        public IEnumerable<NewsInfo> lbzx { get; set; }
        public IEnumerable<NewsInfo>  nw{ get; set; }
    }
}