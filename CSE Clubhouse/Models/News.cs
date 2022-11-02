using System.ComponentModel.DataAnnotations;

namespace CSE_Clubhouse.Models
{
	public class News
	{
		public int Id { get; set; }
		[DataType(DataType.MultilineText)]
		public string NewsContent { get; set; }
		public DateTime CreateTime { get; set; } = DateTime.Now;
	}
}
