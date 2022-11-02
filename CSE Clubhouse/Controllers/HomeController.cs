using CSE_Clubhouse.Areas.Identity.Data;
using CSE_Clubhouse.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CSE_Clubhouse.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly UserManager<ClubhouseUser> _userManager;

		public HomeController(ILogger<HomeController> logger, UserManager<ClubhouseUser> userManager)
		{
			_logger = logger;
			_userManager = userManager;
		}

		public async Task<IActionResult> UserProfile(string Id)
		{
			var user = await _userManager.FindByIdAsync(Id);
			if (user == null)
			{
				return NotFound();
			}
			var model = new UserProfileView
			{
				UserId = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				UserName = user.UserName,
				PhoneNumber = user.PhoneNumber,
				Position = user.Position,
				Email = user.Email,
				CollegeId = user.CollegeID,
				BirthDay = user.BirthDay,
				Admission = user.AdmissionYear,
				ProfilePicture = user.ProfilePicture
			};
			return View(model);
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult About()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}