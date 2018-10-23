using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DepartmentPublicWorks.Controllers
{
    public class ClientZoneController : Controller
    {
        // GET: ClientZone
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Blog()
		{
			return View();
		}
	}
}