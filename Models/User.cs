using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XLN_Fault_Report_System.Models
{
	public class User
	{
		[Key]
		public int UserId { get; set; }

		[Required(ErrorMessage = "Please Enter Username")]
		[Display(Name = "Please Enter Username")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Please Enter Password")]
		[Display(Name = "Please Enter Password")]
		public string Password { get; set; }
		public int isActive { get; set; }
		//public virtual ICollection<Asset> Assets { get; set;}
	}
}
