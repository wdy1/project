using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Famer.Entity.Model;
using Famer.Entity.Abstract;
using Famer.WebUI.Models;


namespace Famer.WebUI.Controllers
{
    public class FreshNewsController : Controller
    {
        // GET: FreshNews
        private IFreshNewsRepository repository;
        public FreshNewsController(IFreshNewsRepository repo)
        {
            repository = repo;
        }
        public ActionResult Index()
        {
            IEnumerable<NewsInfo> TJDD = repository.FreshNews.OrderByDescending(e => e.ClikNum).Take(5);
            IEnumerable<NewsInfo> TJDD1 = repository.FreshNews.OrderByDescending(e => e.ClikNum).Skip(5).Take(7);
            IEnumerable<NewsInfo> JSZQ = repository.FreshNews.Where(e => e.Type == "技术专区").Take(5);
            IEnumerable<NewsInfo> JSZQ1 = repository.FreshNews.Where(e => e.Type == "技术专区").Skip(5).Take(7);
            IEnumerable<NewsInfo> XXZX = repository.FreshNews.OrderByDescending(e => e.Time).Take(8);
            IEnumerable<NewsInfo> NGWH = repository.FreshNews.Where(e => e.Type == "农耕文化").Take(3);
            IEnumerable<NewsInfo> NGWH1 = repository.FreshNews.Where(e => e.Type == "农耕文化").Skip(3).Take(5);
            IEnumerable<NewsInfo> NYZH = repository.FreshNews.Where(e => e.Type == "农业展会").Take(2);
            IEnumerable<NewsInfo> NYZH1 = repository.FreshNews.Where(e => e.Type == "农业展会").Skip(2).Take(5);
            IEnumerable<NewsInfo> WDLJ = repository.FreshNews.Where(e => e.Type == "我的老家").Take(3);
            IEnumerable<NewsInfo> GJTS = repository.FreshNews.Skip(rand()).Take(5);
            IEnumerable<NewsInfo> LBZX = repository.FreshNews.Where(e => e.ClassName == "轮播资讯").Take(4);
            IEnumerable<NewsInfo> LB = repository.FreshNews.Where(e => e.ClassName == "轮播").Take(3);
            IEnumerable<NewsInfo> nw = repository.FreshNews;
            return View(
                new FreshNewsViewModel
                {
                    tjdd = TJDD,
                    tjdd1 = TJDD1,
                    xxzx = XXZX,
                    jszq = JSZQ,
                    jszq1 = JSZQ1,
                    ngwh = NGWH,
                    ngwh1 = NGWH1,
                    nyzh = NYZH,
                    nyzh1 = NYZH1,
                    wdlj = WDLJ,
                    gjts = GJTS,
                    lbzx = LBZX,
                    lb = LB,
                    nw = nw
                }
                );
        }
        public int rand()
        {
            Random r = new Random();
            return r.Next(1, 4);
        }
        public ActionResult SellerDetails(int id,string type)
        {
            NewsInfo ni = repository.FreshNews.Where(e => e.NewsID == id).FirstOrDefault();
			//var ni = repository.FreshNews.FirstOrDefault()
			IEnumerable<NewsInfo> three = repository.FreshNews.Skip(rand()).Take(3);
            return View(new FreshNewsDetailViewModel()
			{
				three=three,
				xiangqing=ni
			}
			);
        }
    }
}