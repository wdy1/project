using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Famer.Entity.Abstract;
using Famer.Entity.Model;
using Famer.Entity.ViewModel;
using static Famer.WebUI.LoginAttributes;

namespace Famer.WebUI.Controllers
{
    public class UserController : Controller
    {
		private IUserInfoRepository repository;
		private IOrderRepository OrderRepo;
		public UserController(IUserInfoRepository repo,IOrderRepository repo1)
		{
			repository = repo;
			OrderRepo = repo1;
		}
        // GET: User
        public ActionResult Info(int UserId)
        {
			UserInfo info = repository.UserInfos.Where(e=>e.UserID==UserId).FirstOrDefault();
            if(info!=null)
			{
				return View(info);
			}
			else
			{
				return RedirectToRoute(new { controller = "Login", action = "Index" });
			}
        }
		[HttpPost]
		public ActionResult Info(UserInfoViewModel uivm)
		{
			repository.SaveInfo(uivm);
			UserInfo user = repository.UserInfos.Where(e => e.UserID == uivm.UserId).FirstOrDefault();
			return View(user);
		}

		//上传自定义头像
		[HttpPost]
		public ActionResult UploadHeaderImg(int UserId,HttpPostedFileBase Image=null)
		{
			UserInfo info = repository.UserInfos.Where(e => e.UserID == UserId).FirstOrDefault();
			if(Image!=null && info!=null)
			{
				info.ImageData = new byte[Image.ContentLength];
				info.ImageMineType = Image.ContentType;
				Image.InputStream.Read(info.ImageData, 0, Image.ContentLength);
			}
			repository.SaveHeaderImg(info);
			TempData["Message"] = String.Format("您的头像已经修改成功");
			return RedirectToAction("Info",new { UserId=UserId});
		}

		//获取用户头像
		public FileContentResult GetImage(int UserId)
		{
			UserInfo info = repository.UserInfos.Where(e => e.UserID == UserId).FirstOrDefault();
			if(info!=null)
			{
				return File(info.ImageData, info.ImageMineType);
			}
			else
			{
				return null;
			}
		}

		[Login]
		public ActionResult MyOrder()
		{
			int UserId = ((UserInfo)Session["userinfo"]).UserID;
			IEnumerable<Order> MyOrders = OrderRepo.Orders.Where(e => e.User_Id == UserId);
			return View(MyOrders);
		}
    }
}