using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CSE_Clubhouse.Areas.Identity.Data;
namespace CSE_Clubhouse
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
               var connectionString = builder.Configuration.GetConnectionString("ClubhouseContextConnection") ?? throw new InvalidOperationException("Connection string 'ClubhouseContextConnection' not found.");

            builder.Services.AddDbContext<ClubhouseContext>(options =>
			{
                options.UseSqlServer(connectionString);
			});

			builder.Services.AddIdentity<Areas.Identity.Data.ClubhouseUser, IdentityRole>(options =>
			{
				options.User.RequireUniqueEmail = false;
			})
			.AddEntityFrameworkStores<ClubhouseContext>()
			.AddDefaultUI()
			.AddDefaultTokenProviders();

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddRazorPages();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();
               app.UseAuthentication();;

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			var services = app.Services.CreateScope().ServiceProvider;
			RoleSeed(services).GetAwaiter().GetResult();
			ContextSeed.SeedSuperAdminAsync(services.GetRequiredService<UserManager<ClubhouseUser>>()).GetAwaiter().GetResult();

			app.MapRazorPages();

			app.Run();
		}

		public async static Task RoleSeed(IServiceProvider services)
		{
			var loggerFactory = services.GetRequiredService<ILoggerFactory>();
			try
			{
				var context = services.GetRequiredService<ClubhouseContext>();
				var userManager = services.GetRequiredService<UserManager<ClubhouseUser>>();
				var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
				await ContextSeed.SeedRolesAsync(userManager, roleManager);
			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "An error occurred seeding the DB.");
			}
		}
	}
}