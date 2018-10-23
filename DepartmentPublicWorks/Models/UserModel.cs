using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DepartmentPublicWorks.Models
{
	public class UserModel
	{
		public int ID { get; set; }
		[Required(ErrorMessage = "First Name is a required")]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Last Name is a required")]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }
		[Required(ErrorMessage = "Email Address is a required")]
		[RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter a valid email Address")]
		[Display(Name = "Email")]
		public string EmailAddress { get; set; }
		[Required(ErrorMessage = "Username is a required")]
		[Display(Name = "Username")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "Password is a required")]
		[Display(Name = "Password")]
		[RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{8,15}", ErrorMessage = "Incorrect password enter a valid password!.")]
		public string Password { get; set; }
		[RegularExpression(@"(?<!\d)\d{10}(?!\d)", ErrorMessage = "Please enter a valid phone number")]
		[Display(Name = "Cellphone Number")]
		public string ContactNumber { get; set; }
		public bool IsActive { get; set; }
	    [Display(Name = "Remember Me")]
		public bool RememberMe { get; set; }
		[Display(Name = "Company Name")]
		public string ClientName { get; set; }
		[Display(Name = "Telephone")]
		public string Telephone { get; set; }
		[Display(Name = "Residetial Address")]
		public string Address { get; set; }
		[Display(Name = "Is Client")]
		public bool IsClient { get; set; }
	}
}