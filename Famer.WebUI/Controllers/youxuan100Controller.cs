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
    public class youxuan100Controller : Controller
    {
        // GET: youxuan100
        private IBetterRepository repository;
        //解析依赖项
        public youxuan100Controller(IBetterRepository repo)
        {
            repository = repo;
        }
        public ActionResult Index()
        {
            IEnumerable<Better100> XXGS = repository.Betters.Where(e => e.TypeName == "新鲜果蔬");
            IEnumerable<Better100> HXSC = repository.Betters.Where(e => e.TypeName == "海鲜水产");
            IEnumerable<Better100> RQDP = repository.Betters.Where(e => e.TypeName == "肉禽蛋品");
            IEnumerable<Better100> LDSS = repository.Betters.Where(e => e.TypeName == "冷冻速食");
			IEnumerable<Better100> top4 = repository.Betters.Where(e => e.TypeName == "冷冻速食").Take(4);
			return View(
                new YouXuanViewModel()
                {
                    xxgs=XXGS,
                    hxsc=HXSC,
                    rqdp=RQDP,
                    ldss=LDSS,
					benqiyouxuan=top4
                }
                );
        }
    }
}