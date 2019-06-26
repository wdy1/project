using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Famer.Entity.Model;
using System.Web.Mvc;

namespace Famer.WebUI.Infrastructure.Binders
{
	//自定义模型绑定器 从当前会话状态获取UserInfo对象
	public class UserInfoModelBinder:IModelBinder
	{
		private const string sessionKey = "UserInfo";

		public object BindModel(ControllerContext controllerContext,ModelBindingContext bindingContext)
		{
			UserInfo userinfo = null;
			if(controllerContext.HttpContext.Session!=null)
			{
				userinfo = (UserInfo)controllerContext.HttpContext.Session[sessionKey];
			}

			if(userinfo==null)
			{
				userinfo = new UserInfo();
				if(controllerContext.HttpContext.Session!=null)
				{
					controllerContext.HttpContext.Session[sessionKey] = userinfo;
				}
			}
			return userinfo;
		} 
	}
}