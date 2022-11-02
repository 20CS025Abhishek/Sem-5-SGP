using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CSE_Clubhouse.Areas.Identity.Data;
using CSE_Clubhouse.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace CSE_Clubhouse.Controllers
{
    public class ClubsController : Controller
    {
        private readonly ClubhouseContext _context;
        private readonly DbSet<ClubMember> _clubhouseContexts;
        private readonly UserManager<ClubhouseUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ClubsController(ClubhouseContext context, UserManager<ClubhouseUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_context = context;
			_userManager = userManager;
			_roleManager = roleManager;
			_clubhouseContexts = _context.Set<ClubMember>();
		}

		[TempData]
        public string StatusMessage { get; set; } = string.Empty;

        // GET: Clubs
        public async Task<IActionResult> Index()
        {
              return View(await _context.Club.ToListAsync());
        }

        [Authorize]
        public async Task<ActionResult> Apply(int? id)
        {
            Club club = _context.Find<Club>(id);
            var user = await _userManager.GetUserAsync(User);
            if (club == null || user == null)
            {
                return NotFound();
            }
            bool isValid = _clubhouseContexts.Any(c => c.UserId == user.Id);
            if (isValid)
            {
                StatusMessage = "You have already applied for this Club!";
                return RedirectToAction("Details", "Clubs", new { id = id });
            }
            ClubMember clubmember = new ClubMember();
            clubmember.ClubId = (int)id;
            clubmember.UserName = user.UserName;
            clubmember.UserId = user.Id;
            _context.Add(clubmember);
            await _context.SaveChangesAsync();
            StatusMessage = "Successfully applied!";
            return RedirectToAction("Details", "Clubs", new { id = id });
        }

        // GET: Clubs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Club == null)
            {
                return NotFound();
            }

            var club = await _context.Club
                .FirstOrDefaultAsync(m => m.Id == id);
            if (club == null)
            {
                return NotFound();
            }

            ViewBag.Message = StatusMessage;
            return View(club);
        }

        [Authorize(Roles = "Admin")]
        // GET: Clubs/Create
        public IActionResult Create()
        {
            return View();
        }

		// POST: Clubs/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Tagline,Description,Goals,Domains,Location,ContactPerson,Email,ContactNo,UpdatedDate,IsDeleted")] Club club)
        {
            if (ModelState.IsValid)
            {
                _context.Add(club);
                await _context.SaveChangesAsync();
                var modName = club.Name + "Moderator";
                club.ClubModRole = modName;
                await _context.SaveChangesAsync();
                await _roleManager.CreateAsync(new IdentityRole(modName.Trim()));
                return RedirectToAction(nameof(Index));
            }
            return View(club);
        }

        [Authorize(Roles = "Admin, ClubModerator")]

        // GET: Clubs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Club == null)
            {
                return NotFound();
            }

            var club = await _context.Club.FindAsync(id);
            if (club == null)
            {
                return NotFound();
            }
            return View(club);
        }

        // POST: Clubs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, ClubModerator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Tagline,Description,Goals,Domains,Location,ContactPerson,Email,ContactNo,UpdatedDate,IsDeleted")] Club club)
        {
            if (id != club.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(club);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClubExists(club.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(club);
        }

        [Authorize(Roles = "Admin")]
        // GET: Clubs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Club == null)
            {
                return NotFound();
            }

            var club = await _context.Club
                .FirstOrDefaultAsync(m => m.Id == id);
            if (club == null)
            {
                return NotFound();
            }

            return View(club);
        }

        [Authorize(Roles = "Admin")]
        // POST: Clubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Club == null)
            {
                return Problem("Entity set 'ClubhouseContext.Club'  is null.");
            }
            var club = await _context.Club.FindAsync(id);
            if (club != null)
            {
                _context.Club.Remove(club);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClubExists(int id)
        {
          return _context.Club.Any(e => e.Id == id);
        }
    }
}
