using System.ComponentModel.DataAnnotations;

namespace BlazorToDoList.Model
{
	public class RegisterModel
	{
		[Required]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		public string ConfirmPassword { get; set; }
	}
}
