using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DepartmentPublicWorks.Models
{
	public class AddressModel
	{
		public int ID { get; set; }
		public string IPAddress { get; set; }
		public string Logitude { get; set; }
		public string Latidute { get; set; }
		public int UserID { get; set; }
		public DateTime DateLogged { get; set; }
	}
}