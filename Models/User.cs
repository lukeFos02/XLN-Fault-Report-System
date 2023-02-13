using System.ComponentModel.DataAnnotations;
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
		public List<Asset> Assets { get; set; }
	}
}
