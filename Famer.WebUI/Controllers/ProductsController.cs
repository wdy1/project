using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Famer.Entity.Abstract;
using Famer.Entity.Model;
using Famer.WebUI.Models;
using Famer.WebUI.Infrastructure;
using static Famer.WebUI.LoginAttributes;

namespace Famer.WebUI.Controllers
{
    public class ProductsController : Controller
    {
		private ISellerDetails Selrepository;
		private ICareChosenRepository Carrepository;
		private IProductsRepository Prorepository;
		private IOrderRepository OrderRepository;
		private ICommentRepository CommentRepository;
		//解析依赖项获取产品类绑定实例
		public ProductsController(ISellerDetails repo,ICareChosenRepository repo1,IProductsRepository repo2,IOrderRepository repo3,ICommentRepository repo4)
		{
			Selrepository = repo;
			Carrepository = repo1;
			Prorepository = repo2;
			OrderRepository = repo3;
			CommentRepository = repo4;
		}
        public ActionResult Index(int ProductId)
        {
			IEnumerable<SellerDetails> ProductLists = Selrepository.ProductLists.Where(m => m.Product_Id == ProductId);
			IEnumerable<CareChosen> CareLists = Carrepository.CareLists.Where(m => m.Product_Id == ProductId);
            return View(new ProductsViewModel() {
				SD=ProductLists,
				CC=CareLists
			});
        }
		//商品详情页
		public ActionResult Details(int ListId,int ProductId)
		{
			SellerDetails sel = Selrepository.ProductLists.Where(m => m.ListID == ListId).FirstOrDefault();
			Products p = Prorepository.Products.Where(m => m.ProductID == ProductId).FirstOrDefault();
			UserInfo info = (UserInfo)Session["userinfo"];
			if(sel!=null)
			{
				OrderViewModel ovm = new OrderViewModel
				{
					Image = sel.Image,
					ProductName = sel.Product_Name,
					Price = sel.Price,
					ShopId = sel.Shop_Id,
					UserId = info.UserID,
					UserName = info.UserName,
					Address = info.Address,
					Phone = info.Phone,
					CommitNum = sel.CommitNum,
					ProductDes = sel.ProductDes,
					KindName = p.KindsClass.Category_Of_Product,
					ListId=ListId,
					ProductId=ProductId
				};
				return View(ovm);
			}
			return View();
		}
		//局部刷新评论分布图
		public ActionResult CommentSummery(int ListId,string Content=null)
		{
			int UserId = ((UserInfo)Session["userinfo"]).UserID;
			if(Content!=null)
			{
				Comment c = new Comment()
				{
					Com_Content = Content,
					Com_Time = DateTime.Now,
					User_Id = UserId,
					List_Id = ListId,
				};
				CommentRepository.AddComment(c);
			}
			//针对某一商品查出所有的评论
			IEnumerable<Comment> comments = CommentRepository.Comments.Where(e => e.List_Id == ListId);
			return PartialView(comments);
		}
		//生成订单
		[Login]
		[HttpPost]
		public ActionResult PlaceAnOrder(int UserId, string UserName, string Address, string Phone, string ShopId,int ListId,int ProductId,int Total)
		{
			Order order = new Order
			{
				User_Id = UserId,
				User_Name = UserName,
				Address = Address,
				Phone = Phone,
				Shop_Id = ShopId,
				OrderTime = DateTime.Now,
				TotalPrice=Total,
			};
			OrderRepository.CreateOrder(order);
			return RedirectToAction("Details",new { ListId = ListId,ProductId = ProductId});
		}
		//搜索框
		[HttpPost]
		public ActionResult Search(string word)
		{
			IEnumerable<SellerDetails> ProductLists = Selrepository.ProductLists.Where(m => m.Product_Name.IndexOf(word) > -1);
			IEnumerable<CareChosen> CareLists = Carrepository.CareLists.Where(m => m.Product_Name.IndexOf(word) > -1);
			if(ProductLists!=null || CareLists!=null)
			{
				return View("Index", new ProductsViewModel()
				{
					SD = ProductLists,
					CC = CareLists
				});
			}
			else
			{
				return View();
			}
		}
    }
}