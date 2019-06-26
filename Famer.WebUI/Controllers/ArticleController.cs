using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Famer.Entity.Model;
using Famer.Entity.Abstract;
using Famer.Entity.Concrete;


namespace Famer.WebUI.Controllers
{
	public class ArticleController : Controller
	{

		FamerEntities FEContext = new FamerEntities();
		// GET: Article
		public ActionResult Index()
		{
			var articles = (from p in FEContext.Article select p).OrderByDescending(p => p.Public_Time).Take(20);
			return View(articles);
		}
		public ActionResult Show()
		{
			return View();
		}

		public ActionResult Details()
		{
			return View();
		}
		public ActionResult Forum()
		{
			var topic = FEContext.Topic.ToList();
			return View(topic);
		}

		//    public ActionResult ForumSummery(int ListId,string Content1=null)
		//    {
		//        int UserId = ((UserInfo)Session["userinfo"]).UserID;
		//        if(Content1 !=null)
		//        {
		//            Comment_Top ct = new Comment_Top()
		//            {
		//                Content = Content1,AddTime = DateTime.Now,UserID = UserId,TopicID = ListId
		//            };

		//        }
		//        IEnumerable<Comment_Top> comment_Top = CommentRepository.comment_Top.Where(e => e.TopicID == ListId);
		//        return PartialView(comment_Top);
		//    }
		//}
	}
}