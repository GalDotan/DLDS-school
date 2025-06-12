using System.ComponentModel.DataAnnotations;

namespace BlazorToDoList.Model
{
	public class LoginModel
	{
		[Required]
		public string? Username { get; set; }

		[Required]
		public string? Password { get; set; }
	}
}
