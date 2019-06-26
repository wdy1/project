using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Famer.Entity.Model;
using Famer.Entity.ViewModel;
using PagedList;
using PagedList.Mvc;
using System.Net;

namespace Famer.WebUI.Controllers
{
    public class ProviderController : Controller
    {
        // GET: Provider

        FamerEntities FEContext = new FamerEntities();
        public ActionResult Index(int? page ,string genreInfoFrom ,string SortInfoFrom ,string currentFilter)
        {
            int pageSize = 15;
            int pageNum = (page ?? 1);
            var mc = FEContext.MainClass.ToList();
            var kc = FEContext.KindsClass.ToList();
            var ad = FEContext.Address2.ToList();       
            var supinfo = (from p in FEContext.SupImformation select p).OrderByDescending(p => p.Sup_Time).Take(100);            
            //种类显示
            if (genreInfoFrom != null)
            {
                page = 1;
            }
            else
            {
                genreInfoFrom = currentFilter;
            }
            ViewBag.CurrentFilter = genreInfoFrom;

            //地区显示
            if (SortInfoFrom != null)
            {
                page = 1;
            }
            else
            {
                SortInfoFrom = currentFilter;
            }
            ViewBag.CurrentFilter = SortInfoFrom;
            
            //按条件分类好的供应信息传递给supinfo；
            if (!String.IsNullOrEmpty(genreInfoFrom))
            {
                supinfo = supinfo.Where(p => p.SupContent == genreInfoFrom);
            }
            if (!String.IsNullOrEmpty(SortInfoFrom))
            {
                supinfo = supinfo.Where(p => p.Address_Of_Sup == SortInfoFrom);
            }

            return PartialView(supinfo.ToPagedList(pageNum, pageSize));               
        }

        public ActionResult Details(int id)
        {            
            var users = FEContext.UserInfo.FirstOrDefault();
            var sup = FEContext.SupImformation.Where(m => m.SupplyID == id).FirstOrDefault();           
            var com = (from p in FEContext.Comment_Sup select p).OrderByDescending(p => p.Addtime);
            var model = new ProviderViewModel() { Sup = sup,comment_sup = com,user = users};
            return View(model);
        }

		public ActionResult Search(string searchString, string currentFilter, int? page)
		{
			var sup1 = from m in FEContext.SupImformation.OrderByDescending(p => p.Sup_Time)

					   select m;
			if (searchString != null)
			{
				page = 1;
			}
			else
			{
				searchString = currentFilter;
			}

			ViewBag.CurrentFilter = searchString;

			if (!String.IsNullOrEmpty(searchString))
			{

				sup1 = sup1.Where(s => (s.Sup_Of_Name.Contains(searchString) || s.Address_Of_Sup.Contains(searchString) || s.SupContent.Contains(searchString)));
			}
			int pageSize = 9;
			int pageNumber = (page ?? 1);

			return View(sup1.ToPagedList(pageNumber, pageSize));
		}


		public PartialViewResult GetDescription(int id)
        {
            var users = FEContext.UserInfo.FirstOrDefault();      
            var sup = FEContext.SupImformation.Where(m => m.SupplyID == id).FirstOrDefault();                   
            var model5 = new ProviderViewModel { Sup = sup, user = users};
            return PartialView(model5);
        }

        //public PartialViewResult GetComment(int supId,string content = null)
        //{
        //    int UserId = ((UserInfo)Session["userinfo"]).UserID;
        //    if (content != null)
        //    {
        //        Comment_Sup cs = new Comment_Sup()
        //        {
        //            Com_Content = content,
        //            Addtime = DateTime.Now,
        //            UserID = UserId,
        //            SupplyID = supId
        //        };
        //        CommentRepository.AddComment1(cs);
        //    }
        //    IEnumerable<Comment_Sup> comment_Sup = CommentRepository.comment_Sup.Where(e => e.SupplyID == supId);
        //    return PartialView(comment_Sup);          
        //    //string pingluntextarea = Request["pingluntextarea"];
        //    //int supplyId = Convert.ToInt32(Request["supplyId"]);
        //    //if (ModelState.IsValid)
        //    //{
        //    //    comment_Sup.SupplyID = supplyId;
        //    //    comment_Sup.UserID = Convert.ToInt32(Session["UserID"].ToString());
        //    //    comment_Sup.Content = pingluntextarea;
        //    //    comment_Sup.Addtime = System.DateTime.Now;
        //    //    FEContext.Comment_Sup.Add(comment_Sup);
        //    //    FEContext.SaveChanges();        
        //    //}
        //    //return PartialView("GetComment", "Provider");            
        //}

        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public ActionResult Comment(Comment_Sup comment_Sup)
        //{
        //    string pingluntextarea = Request["pingluntextarea"];
        //    int supplyID = Convert.ToInt32(Request["supplyID"]);       
        //    if (ModelState.IsValid)
        //    {
        //        comment_Sup.SupplyID = supplyID;
        //        comment_Sup.UserID = Convert.ToInt32(Session["UserID"]);
        //        comment_Sup.Content = pingluntextarea;
        //        comment_Sup.Addtime = System.DateTime.Now;
        //        FEContext.Comment_Sup.Add(comment_Sup);                       
        //        FEContext.SaveChanges();
        //        return Content("<script>;alert('评论成功!');history.go(-1)</script>");

        //    }

        //    return RedirectToAction("Details", "Provider");
        //    //return View(comment_Food);
        //}
        public ActionResult Sort()
        {
            var mc = FEContext.MainClass.ToList();
            var kc = FEContext.KindsClass.ToList();
            var ad = FEContext.Address2.ToList();
            var model = new ProviderViewModel()
            {
                address2 = ad,
                mainclass = mc,
                kindsclass = kc,
            };
            return View(model);
        }
        public ActionResult Show()
        {
            return PartialView();
        }

    }
}