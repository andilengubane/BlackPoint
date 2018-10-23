using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.Mvc;
using System.Diagnostics;
using DataAccess;
using DepartmentPublicWorks.Models;

namespace DepartmentPublicWorks.Controllers
{
	public class AccountController : Controller
	{
		DepartmentOfPublicWorksEntities context = new DepartmentOfPublicWorksEntities();

		public ActionResult Login()
		{ return View(); }
		public ActionResult RecoverPassword()
		{ return View(); }
		public ActionResult Register()
		{ return View(); }

		[HttpPost]
		public ActionResult GetUserAccess(UserModel model)
		{
			if (model.UserName != null)
			{
				var data = context.Users.FirstOrDefault(u => u.UserName == model.UserName && u.Password == model.Password);
				if (data != null)
				{
					this.Session["ID"] = data.ID;
					this.Session["UserName"] = data.UserName;
					this.Session["FirstName"] = data.FirstName;
					this.Session["LastName"] = data.LastName;
					ViewBag.Details = this.Session["FirstName"] + " " + this.Session["LastName"];
					return RedirectToAction("Index", "DashBord");
				}
				else
				{
					ViewBag.ErroMessage = "Your logged in details " + model.UserName + " and " + model.Password + "is incorrect";
				}
			}
			{
				ViewBag.ErroMessage = "Please provide your login detaial " + model.UserName + " and " + model.Password + "are missing";
			}
			return View("Login");
		}
	
		public ActionResult AddNewUser(UserModel model)
		{
			var data = context.Users.FirstOrDefault(u => u.EmailAddress == model.EmailAddress && u.ContactNumber == model.ContactNumber);
			if (data != null)
			{
				ViewBag.ErroMessage = "Details provided already exist " + data.EmailAddress + " and " + model.ContactNumber + " .";
			}
			else
			{
				if (ModelState.IsValid)
				{
					context.Users.Add(new User
					{
						UserName = model.UserName,
						Password = model.Password,
						LastName = model.LastName,
						FirstName = model.FirstName,
						EmailAddress = model.EmailAddress,
						DateLogged = DateTime.Now,
						ContactNumber = model.ContactNumber,
						IsActive = true
					});
					context.SaveChanges();

					MailMessage msg = new MailMessage();
					msg.From = new MailAddress("mthembungubane@gmail.com");
					msg.To.Add(new MailAddress(model.EmailAddress));
					msg.Subject = "Welcom to Black Point";
					msg.Body = "Your login details  " + 
						       " Username : " + model.UserName  + 
							   " Password : " + model.Password + 
							   " You can also downlaod mobile for BlacK Point." +
							   " Lets help our goverment with daily services he provide for us report all fautl and missed used goverment resources";

					SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
					smtpClient.Port = 587;
					smtpClient.UseDefaultCredentials = false;
					System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("mthembungubane@gmail.com", "andile1234!@#$");
					smtpClient.Credentials = credentials;
					smtpClient.EnableSsl = true;
					smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
					smtpClient.Send(msg);
					return this.RedirectToAction("Index", "Orders");
				}
			}
			return this.View("Register", model);
		}
		public ActionResult ForgotPassword(UserModel model)
		{
			if (model.EmailAddress != null)
			{
				try
				{
					var result = from user in context.Users
								 where user.EmailAddress == model.EmailAddress
								 select user;
					foreach (var item in result)
					{
						var password = item.Password;
						var emailAddres = item.EmailAddress;
						this.sendMail(password, emailAddres);
					}
				}
				catch (Exception ex)
				{
					string error = ex.Message;
				}
				ViewBag.ErrorMessage = "Please check your email Address";
				return this.View("Login");
			}
			return this.View("RecoverPassword", model);
		}
		public void sendMail(string password, string emailAddress)
		{
			MailMessage msg = new MailMessage();
			msg.From = new MailAddress("joe@contoso.com");
			msg.To.Add(new MailAddress(emailAddress));
			msg.Subject = "Recover Password";
			msg.Body = " Please make sure you don't you keep your password" + password;

			SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
			System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("joe@contoso.com", "XXXXXX");
			smtpClient.Credentials = credentials;
			smtpClient.EnableSsl = true;
			smtpClient.Send(msg);
		}

	}
}
