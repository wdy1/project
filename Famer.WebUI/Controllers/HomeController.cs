using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Famer.Entity.Model;
using Famer.Entity.Abstract;
using Famer.Entity.Concrete;
using Famer.WebUI.Models;

namespace Famer.WebUI.Controllers
{
    public class HomeController : Controller                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
    {
		private IUserInfoRepository repository;
		private ISellerDetails SelRepo;

		//Ninject解析依赖项  生成实现了IUserInfo接口对象的实例
		public HomeController(IUserInfoRepository repo,ISellerDetails repo1)
		{
			repository = repo;
			SelRepo = repo1;
		}
        // GET: Home
        public ActionResult Index(UserInfo info)
        {
			IEnumerable<SellerDetails> Mor = SelRepo.ProductLists.Where(e => e.Morn == 1);
			IEnumerable<SellerDetails> Noon = SelRepo.ProductLists.Where(e => e.Morn == 2);
			IEnumerable<SellerDetails> Even = SelRepo.ProductLists.Where(e => e.Morn == 3);
			return View(new HomeViewModel()
			{
				Mor = Mor,
				Noon = Noon,
				Even = Even
			});
        }

		public PartialViewResult LoginSummary()
		{
			return PartialView(Session["userinfo"]);
		}

		//退出登录
		public RedirectToRouteResult LoginOut()
		{
			Session["userinfo"] = null;
			Session["cart"] = null;
			return RedirectToRoute(new { controller = "Home", action = "Index" });
		}

    }
}