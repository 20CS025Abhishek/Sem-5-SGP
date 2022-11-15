using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSE_Clubhouse.Models
{
	public class Club
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string? Tagline { get; set; }
		[Required]
		[DataType(DataType.MultilineText)]
		public string Description { get; set; }
		public string? Goals { get; set; }
		public string? Domains { get; set; }
		public string? Location { get; set; }
		public string? ContactPerson { get; set; }
		[DataType(DataType.EmailAddress)]
		public string? Email { get; set; }
		[DataType(DataType.PhoneNumber)]
		public string? ContactNo { get; set; }
		public DateTime? CreatedDate { get; }
		public DateTime? UpdatedDate { get; set; }
		public bool IsDeleted { get; set; }
		public string? ClubModRole { get; set; }
		// Logo
		public string? CoverImgURL { get; set; }
		[NotMapped]
		[Display(Name="Cover Image")]
		public IFormFile? CoverImg { get; set; }
		// Slideshow Images
	}
}
