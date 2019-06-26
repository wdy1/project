using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Famer.Entity.Model;
using Famer.Entity.Abstract;
using Famer.Entity.ViewModel;
using Famer.WebUI.Models;

namespace Famer.WebUI.Controllers
{
    public class SellerController : Controller
    {
		// GET: Seller
		private IShopRepository repository;
		private IUserInfoRepository userrepository;
		//解析依赖项获取商店表数据
		public SellerController(IShopRepository repo,IUserInfoRepository repo1)
		{
			repository = repo;
			userrepository = repo1;
		}
        public ActionResult Index()
        {
			Shop shop = (Shop)Session["shop"];
			if (Session["userinfo"] == null)//如果用户未登录 则返回登录视图
			{
				return RedirectToRoute(new { controller = "Login", action = "Index" });
			}
			else if (((UserInfo)Session["userinfo"]).UserRole == "普通用户")
			{
				return View("ERSeller", (UserInfo)Session["userinfo"]);
			}
			else if (shop == null)
			{
				return View("Login");
			}
			else
				return View(shop);
        }
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(LoginViewModelForSel lvms)
		{
			if(repository.CheckLoginForSel(lvms))
			{
				Session["shop"] = (Shop)repository.Shops.Where(m => m.Contact == lvms.Phone).FirstOrDefault();
				return View("Index");
			}
			else
			{
				ModelState.AddModelError("Password", "您输入的账户名和密码不匹配，请重新输入");
				return View();
			}
		}

		//当用户为普通用户时返回的视图
		public ActionResult ERSeller(UserInfo info)
		{
			return View(info);
		}

		public ActionResult Enter()
		{
			UserInfo ui = (UserInfo)Session["userinfo"];
			return View(ui);
		}
		
		[HttpPost]
		public ActionResult Enter(ShopFormViewModel sfvm)
		{
			Shop shop = repository.Shops.Where(m => m.Contact == sfvm.Phone).FirstOrDefault();
			if(shop!=null)
			{
				ModelState.AddModelError("Phone", "该手机号已被注册为商家");
				return View();
			}
			else
			{
				repository.AddShop(new Shop
				{
					ShopName = sfvm.ShopName,
					Seller_Id = ((UserInfo)Session["userinfo"]).UserID,
					Contact = sfvm.Phone,
					Address = sfvm.Address,
					IDcard = sfvm.IDcard,
					Password = sfvm.Password,
					BossName = sfvm.BossName,
					CreateTime = DateTime.Now,
					ShopID=ShopID()
				});
				//改变用户身份
				userrepository.ChangeRole((UserInfo)Session["userinfo"]);
				return View("Login");
			}
		}
		public string ShopID()
		{
			Random r = new Random();
			return r.Next(100000, 10000000).ToString();
		}

    }
}