using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DepartmentPublicWorks.Models
{
	public class DashBoardModel
	{
		public IEnumerable<SelectListItem>  StatusList { get; set; }
		public int ID { get; set; }
		public string Description { get; set; }
		public string Status { get; set; }
		public DateTime DateLogged { get; set; }
		public string ImageUrl { get; set; }
		public int UserID { get; set; }
	}
}