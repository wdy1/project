using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Famer.Entity.ViewModel;
using Famer.Entity.Concrete;
using Famer.Entity.Abstract;
using Famer.Entity.Model;

namespace Famer.WebUI.Controllers
{
    public class LoginController : Controller
    {
		// GET: Login
		private IUserInfoRepository repository;
		private ICartRepository cart;
		//解析依赖项
		public LoginController(IUserInfoRepository repo,ICartRepository cartRe)
		{
			repository = repo;
			cart = cartRe;
		}
        public ActionResult Index() 
        {
            return View();
        }

		[HttpPost]
		public ActionResult Index(LoginViewModel lvm)
		{
			if(repository.CheckLogin(lvm))
			{
				UserInfo info = repository.UserInfos.Where(m => m.Phone == lvm.Phone).FirstOrDefault();
				Session["userinfo"] = info;
				//初始化购物车嗷
				Cart cart1 = cart.Carts.Where(e => e.User_Id == info.UserID).FirstOrDefault();
				//如果为空则表示是第一次登录 
				if(cart1==null)
				{
					Cart cart2 = new Cart { User_Id = info.UserID, Num = 0, Total = 0 };
					//把用户的购物车存入数据库
					cart.AddCart(cart2);
					Session["cart"] = cart2;
				}
				else
				{
					Session["cart"] = cart1;
				}
				return RedirectToRoute(new { controller = "Home", action = "Index" });
			}
			else
			{
				ModelState.AddModelError("Password", "密码验证错误");
				return View();
			}
		}
    }
}