using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Famer.Entity.Model;
using Famer.Entity.Abstract;
using Famer.Entity.Concrete;
using Famer.Entity.ViewModel;
using PagedList;

namespace Famer.WebUI.Controllers
{
    public class RequirementController : Controller
    {
        FamerEntities FEContext = new FamerEntities();
        // GET: Requirement
        public ActionResult Index(int? page, string genreInfoFrom1, string SortInfoFrom1, string currentFilter1)
        {
            int pageSize = 20;
            int pageNum = (page ?? 1);
            var mc = FEContext.MainClass.ToList();
            var kc = FEContext.KindsClass.ToList();
            var ad = FEContext.Address2.ToList();
            var pulinfo = (from p in FEContext.PurImformation select p).OrderByDescending(p => p.Pur_Time).Take(1000);
            //种类显示
            if (genreInfoFrom1 != null)
            {
                page = 1;
            }
            else
            {
                genreInfoFrom1 = currentFilter1;
            }
            ViewBag.CurrentFilter = genreInfoFrom1;

            //地区显示
            if (SortInfoFrom1 != null)
            {
                page = 1;
            }
            else
            {
                SortInfoFrom1 = currentFilter1;
            }
            ViewBag.CurrentFilter = SortInfoFrom1;

            //按条件分类好的供应信息传递给supinfo；
            if (!String.IsNullOrEmpty(genreInfoFrom1))
            {
                pulinfo = pulinfo.Where(p => p.PurContent == genreInfoFrom1);
            }
            if (!String.IsNullOrEmpty(SortInfoFrom1))
            {
                pulinfo = pulinfo.Where(p => p.Address_Of_Pur == SortInfoFrom1);
            }
            var model = new RequirementViewModel()
            {
                Pulinfo = pulinfo.ToPagedList(pageNum, pageSize),
                mainclass = mc,
                kindsclass = kc,
                address2 = ad
            };
            return View(model);                  
        }
    }
}