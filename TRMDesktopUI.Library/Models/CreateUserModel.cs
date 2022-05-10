using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TRMDesktopUI.Library.Models
{
	public class CreateUserModel
	{
		[Required]
		[DisplayName("First Name")]
		public string FirstName { get; set; }
		[Required]
		[DisplayName("Last Name")]
		public string LastName { get; set; }
		[Required]
		[EmailAddress]
		[DisplayName("Email Address")]
		public string EmailAddress { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		[Compare(nameof(Password), ErrorMessage = "The password do not match")]
		[DisplayName("Confirm Password")]
		public string ConfirmPassword { get; set; }
	}
}
