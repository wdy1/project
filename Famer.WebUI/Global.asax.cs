using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Famer.Entity.Model;
using Famer.WebUI.Infrastructure.Binders;

namespace Famer.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

			//配置绑定
			ModelBinders.Binders.Add(typeof(UserInfo), new UserInfoModelBinder());
        }
    }
}
