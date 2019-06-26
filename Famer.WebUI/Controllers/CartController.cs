using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Famer.Entity.Model;
using Famer.Entity.Abstract;
using Famer.WebUI.Models;
using Famer.WebUI.Infrastructure;
using static Famer.WebUI.LoginAttributes;

namespace Famer.WebUI.Controllers
{
    public class CartController : Controller
    {
		private ICartRepository carts;
		private ICartLineRepository Cartlines;
		//解析依赖项同时获取 购物车和购物车条目集合
		public CartController(ICartRepository cartRepo,ICartLineRepository cartlineRepo)
		{
			carts = cartRepo;
			Cartlines = cartlineRepo;
		}
		//购物车分布视图
		public PartialViewResult Summary()
		{
			Cart MyCart = (Cart)Session["cart"];
			if(MyCart==null)
			{
				return PartialView(null);
			}
			else
			{
				IEnumerable<CartLine> MyCartLines = Cartlines.CartLines.Where(e => e.Cart_Id == MyCart.CartID);
				return PartialView(MyCartLines);
			}
		}

		//购物车页
        public ActionResult Index()
        {
			//int CartId = ((Cart)Session["cart"]).CartID;
			//IEnumerable<CartLine> cartlines = Cartlines.CartLines.Where(e => e.Cart_Id == CartId);

            return View();
        }

		//局部刷新的分布试图
		public ActionResult CartSummary(int? CartLineID)
		{
			int CartId = ((Cart)Session["cart"]).CartID;
			if (CartLineID!=null)
			{
				CartLine cl = Cartlines.CartLines.Where(e => e.CartLineID == CartLineID).FirstOrDefault();
				Cartlines.RemoveItem(cl);
				//重新查找出 购物车列表
				IEnumerable<CartLine> cls = Cartlines.CartLines.Where(e => e.Cart_Id == CartId);
				return PartialView(cls);
			}
			else
			{
				IEnumerable<CartLine> cls = Cartlines.CartLines.Where(e => e.Cart_Id == CartId);
				return PartialView(cls);
			}
		}
		//加入购物车方法
		[Login]
		[HttpPost]
		public ActionResult AddToCart(CartFormViewModel cfvm)
		{
			//获取当前用户的购物车Id
			int CartId = ((Cart)Session["cart"]).CartID;
			try
			{
				IEnumerable<CartLine> cartlines = Cartlines.CartLines.Where(e => e.Cart_Id == CartId);
				//获取正在加入购物车的产品
				CartLine cartline = cartlines.Where(m => m.List_Id == cfvm.ListID).FirstOrDefault();
				if (cartline == null)
				{
					//如果为空 则初始化这个购物车条目
				    cartline = new CartLine
					{
						Product_Id = cfvm.ProductId,
						ProductDes = cfvm.ProductDes,
						Image = cfvm.Image,
						Price = cfvm.Price,
						Cart_Id = CartId,
						List_Id = cfvm.ListID
					};
					Cartlines.AddItem(cartline, 1);
				}
				else
				{
					Cartlines.AddItem(cartline, 1);
				}
				//同时更新购物车表
				carts.UpdateCart((Cart)Session["cart"], cartline);
				return RedirectToAction("Index");
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		//结算购物车选中项 前台传递复选框选中的产品CartLineID数组
		[Login]
		[HttpPost]
		public JsonResult BalanceAccount(int[] CLs)
		{
			for (int i = 0; i < CLs.Length; i++)
			{
				CartLine cl = Cartlines.CartLines.Where(e => e.CartLineID == CLs[i]).FirstOrDefault();
				if (cl != null)
				{
					Cartlines.RemoveItem(cl);
				}
			}
			AjaxMsg msg = new AjaxMsg()
			{
				Statu = "Ok",
				Message = "购买成功!"
			};
			return base.Json(msg);
		}

		////前台ajax传递值
		//[HttpPost]
		//public JsonResult RemoveFromCart(int CartLineID)
		//{
		//	CartLine cl = Cartlines.CartLines.Where(e => e.CartLineID == CartLineID).FirstOrDefault();
		//	Cartlines.RemoveItem(cl);
		//	AjaxMsg ajaxmsg = new AjaxMsg()
		//	{
		//		Statu = "Ok",
		//		Message = "您以成功删除商品"
		//	};
		//	return base.Json(ajaxmsg);
		//}

    }
}