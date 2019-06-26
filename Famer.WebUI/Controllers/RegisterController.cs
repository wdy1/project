using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Famer.Entity.Model;
using System.Net.Mail;
using Famer.WebUI.Models;
using Famer.Entity.Abstract;


namespace Famer.WebUI.Controllers
{
    public class RegisterController : Controller
    {
		private string code = "";
		private IUserInfoRepository repository;

		public RegisterController(IUserInfoRepository repo)
		{
			repository = repo;
		}
        // GET: Register
        public ActionResult Index()
        {
            return View(new RegisterViewModel());
        }

		[HttpPost]
		public ActionResult Index(RegisterViewModel rvm)
		{
			UserInfo info = repository.UserInfos.Where(m => m.Phone == rvm.Phone).FirstOrDefault();
			if(info!=null)
			{
				return View();
			}
			else
			{
				repository.AddUser(new UserInfo
				{
					UserPassword = rvm.Password,
					UserName = rvm.UserName,
					Phone=rvm.Phone,
					Email=rvm.Email,
					RegisterTime=rvm.Time,
					UserRole="普通用户"
				});
				return RedirectToRoute(new { controller = "Home", action = "Index" });
			}
		}
		[HttpPost]
		public string SentMail(string email)
		{
			string returnBool = "false";
			MailMessage mail = new MailMessage();
			SmtpClient smtp = new SmtpClient();
			string strFromEmail = "yuxuan1491433427@qq.com";
			string EmailPassword = "kpugkkcuwekbgiba";
			code = IdentityCode();
			Session["code"] = code;
			try
			{
				mail.From = new MailAddress(strFromEmail);
				mail.To.Add(new MailAddress(email));
				mail.BodyEncoding = Encoding.UTF8;
				mail.SubjectEncoding = Encoding.UTF8;
				mail.Subject = "验证码-助农为乐";
				mail.Body = String.Format("您本次注册的验证码为:{0}",code);
				smtp.Host = "smtp.qq.com";
				smtp.EnableSsl = false;
				smtp.Credentials = new NetworkCredential(strFromEmail, EmailPassword);
				smtp.Send(mail);
				returnBool = "true";
			}
			catch(Exception e)
			{
				returnBool = "false";
			}
			return returnBool;
		}
		[HttpPost]
		public string CheckCode(string codeString)
		{
			string returnBool = "false";
			if(codeString==(string)Session["code"])
			{
				returnBool = "true";
			}
			return returnBool;
		}
		public string IdentityCode()
		{
			Random r = new Random();
			return r.Next(100000, 1000000).ToString();
		}


	}
}