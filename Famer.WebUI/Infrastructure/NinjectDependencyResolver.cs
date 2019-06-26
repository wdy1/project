using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Famer.Entity.Abstract;
using Famer.Entity.Concrete;

namespace Famer.WebUI.Infrastructure
{
	public class NinjectDependencyResolver:IDependencyResolver
	{
		private IKernel kernel;
		public NinjectDependencyResolver(IKernel kernelParam)
		{
			kernel = kernelParam;
			//添加绑定
			AddBindings();
		}

		public object GetService(Type serviceType)
		{
			return kernel.TryGet(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return kernel.GetAll(serviceType);
		}

		private void AddBindings()
		{
			//添加绑定 --用户
			kernel.Bind<IUserInfoRepository>().To<EFUserInfoRepository>();
			//添加绑定 -- 产品
			kernel.Bind<IProductsRepository>().To<EFProductsRepository>();
			//添加绑定 -- 店家产品
			kernel.Bind<ISellerDetails>().To<EFListsRepository>();
			//添加绑定 --精选产品
			kernel.Bind<ICareChosenRepository>().To<EFCareListsRepository>();
			//添加绑定 --店家
			kernel.Bind<IShopRepository>().To<EFShopsRepository>();
			//添加绑定 --购物车
			kernel.Bind<ICartRepository>().To<EFCartRepository>();
			//添加绑定 --购物车条目
			kernel.Bind<ICartLineRepository>().To<EFCartLineRepository>();
			//添加绑定 --订单
			kernel.Bind<IOrderRepository>().To<EFOrdersRepository>();
			//添加绑定 --评论
			kernel.Bind<ICommentRepository>().To<EFCommentRepository>();
			//添加绑定 --优选100
			kernel.Bind<IBetterRepository>().To<EFBetterRepository>();
			//tian
			kernel.Bind<IFreshNewsRepository>().To<EFFreshNewsRepository>();
		}
	}
}