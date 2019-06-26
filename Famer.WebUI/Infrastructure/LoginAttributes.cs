using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Famer.WebUI
{
	public class LoginAttributes
	{
		public class LoginAttribute : ActionFilterAttribute
		{
			public override void OnActionExecuting(ActionExecutingContext filterContext)
			{
				if (filterContext.HttpContext.Session["userinfo"] == null)
				{
					//filterContext.HttpContext.Response.Redirect("/UserInfo/Login");
					//
					ContentResult cr = new ContentResult();
					cr.Content = "<script>alert('您尚未登录，请登录！'); window.window.location.href = '/Login/Index';</script>"; /*$('.denglubox').css('display', 'block');*/
					filterContext.Result = cr;
				}
			}
		}
	}
}
