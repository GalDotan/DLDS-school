namespace BlazorToDoList.Model
{
	public class UserState
	{
		public int UserId { get; set; }
		public string UserName { get; set; }
		public bool IsAdmin { get; set; }
		public void SetUserState(int userId, string userName, bool isAdmin)
		{
			UserId = userId;
			UserName = userName;
			IsAdmin = isAdmin;
		}
	}
}
