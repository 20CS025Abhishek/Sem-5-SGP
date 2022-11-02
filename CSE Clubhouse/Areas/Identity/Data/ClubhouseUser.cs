using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CSE_Clubhouse.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ClubhouseUser class
public class ClubhouseUser : IdentityUser
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	[DataType(DataType.Date)]
	public DateTime? BirthDay { get; set; }
	public string? Gender { get; set; }
	public string? Position { get; set; }
	//public string[] Postions = new string[] { "Student", "Faculty", "Alumni" };
	public string? CollegeID { get; set; }
	public DateTime? AdmissionYear { get; set; }
	public byte[]? ProfilePicture { get; set; }
	public int UsernameChangeLimit { get; set; } = 10;
	[DataType(DataType.DateTime)]
	public DateTime? CreateDate { get; set; }
	[DataType(DataType.DateTime)]
	public DateTime? UpateTime { get; set; }
}

