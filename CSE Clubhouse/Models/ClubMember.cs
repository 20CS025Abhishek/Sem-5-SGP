namespace CSE_Clubhouse.Models
{
	public class ClubMember
	{ 
		public int Id { get; set; }
		public string UserId { get; set; }
		public string UserName { get; set; }
		public int ClubId { get; set; }
		public bool IsConfirmed { get; set; } = false;
		public bool IsLeader { get; set; } = false;
	}
}