using System;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using DataAccess;
using System.Collections.Generic;
using DepartmentPublicWorks.Models;

namespace DepartmentPublicWorks.Controllers
{
	public class DashBordController : Controller
	{
		DepartmentOfPublicWorksEntities context = new DepartmentOfPublicWorksEntities();
		public ActionResult Create()
		{
			var items = context.DataLookUps.Where(x => x.LookUpNameID == 1).ToList();
			if (items != null)
			{
				ViewBag.data = items;
			}
			return View("Create");
		}
		public ActionResult Report()
		{ return View(); }
		public ActionResult Clients()
		{ return View(); }
		public ActionResult Careers()
		{ return View(); }
		public ActionResult Index()
		{
			var model = new List<DashBoardModel>();
			var items = context.DataLookUps.Where(x=>x.LookUpNameID == 1).ToList();
			if (items != null)
			{
				ViewBag.data = items;
			}

			try
			{
				var data = from dashBordList in context.DashBoards
						   select dashBordList;
				foreach (var item in data)
				{
					model.Add(new DashBoardModel()
					{
						ID = item.ID,
						Description = item.Description,
						Status = item.Status,
						DateLogged = item.DateLogged
					});
				}
			}
			catch (Exception ex)
			{
				string error = ex.Message;
			}
			return this.View("Index", model);
		}

		
		public ActionResult Edit(int id)
		{
			var model = new DashBoardModel();
			if (id != 0)
			{
				try
				{
					var data = context.DashBoards.Where(m => m.ID == id);
					foreach (var result in data)
					{
						model.ID = result.ID;
						model.Description = result.Description;
						model.Status = result.Status;
					}
				}
				catch (Exception ex)
				{
					var error = ex.Message;
				}
			}
			return PartialView("Create", model);
		}

		public ActionResult CreateNotification(DashBoardModel model)
		{
			string logedInUserID = Session["ID"].ToString();
			string hostName = Dns.GetHostName();
			string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();

			string logitudeAddress = Dns.GetHostByName(hostName).AddressList[0].ToString();
			string latitudeAddress = Dns.GetHostByName(hostName).AddressList[0].ToString();

			if (model.ID == 0)
			{
				context.DashBoards.Add(new DataAccess.DashBoard
				{
					Description = model.Description,
					UserID = Convert.ToInt32(logedInUserID),
					DateLogged = DateTime.Now,
					Status = model.Status,
					IPAddress = myIP,
					Longitude = logitudeAddress,
					Latitute = latitudeAddress,
				});
				context.SaveChanges();
				return this.PartialView("Index");
			}
			else
			{
				var data = context.DashBoards.FirstOrDefault(m => m.ID == model.ID);
				data.ID = model.ID;
				data.Description = model.Description;
				data.Status = model.Status;
				data.UserID = Convert.ToInt32(logedInUserID);
				data.IPAddress = myIP;
				data.Longitude = logitudeAddress;
				data.Latitute = latitudeAddress;

				return this.PartialView("Index");
			}
		}
	}
}
