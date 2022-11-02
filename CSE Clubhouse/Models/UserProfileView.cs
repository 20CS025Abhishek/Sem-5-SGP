namespace CSE_Clubhouse.Models
{
	public class UserProfileView
	{
		public string UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string? Position { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string? CollegeId { get; set; }
		public DateTime? BirthDay { get; set; }
		public DateTime? Admission { get; set; }
		public byte[]? ProfilePicture { get; set; }
	}
}
