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
    public class ClubMembersController : Controller
    {
        private readonly ClubhouseContext _context;
        private readonly UserManager<ClubhouseUser> _userManager;


        public ClubMembersController(ClubhouseContext context, UserManager<ClubhouseUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ClubMembers
        public async Task<IActionResult> Index()
        {
              return View(await _context.ClubMember.ToListAsync());
        }

        // GET: ClubMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClubMember == null)
            {
                return NotFound();
            }

            var clubMember = await _context.ClubMember
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clubMember == null)
            {
                return NotFound();
            }

            return View(clubMember);
        }

		[Authorize(Roles = "ClubModerator")]
        public async Task<IActionResult> ConfirmMember(int? id)
        {
            var clubmember = _context.Find<ClubMember>(id);
            if (clubmember == null)
            {
                ViewBag.ErrorMessage = $"Club Member with Id = {id} can not be found!";
                return NotFound();
            }
            var club = _context.Find<Club>(clubmember.ClubId);
            if (club == null)
            {
                ViewBag.ErrorMessage = $"Club with Id = {clubmember.ClubId} can not be found!";
                return NotFound();
            }
            if (!clubmember.IsConfirmed)
            {
                clubmember.IsConfirmed = true;
                await _context.SaveChangesAsync();
                return RedirectToAction("ClubMembers", "ClubMembers", new { id = club.Id });
            }
            return RedirectToAction();
        }

        public async Task<IActionResult> ClubMembers(int? id)
        {
            ViewBag.ClubId = id;
            var club = await _context.FindAsync<Club>(id);
            if (club == null)
            {
                ViewBag.ErrorMessage = $"Club with Id = {id} can not be found!";
                return NotFound();
            }
            var clubmembers = new List<ClubMember>();
            var users = new List<ClubhouseUser>();
            foreach(var member in _context.ClubMember)
			{
                if (member.ClubId == club.Id)
                {
                    clubmembers.Add(member);
                    users.Add(await _userManager.FindByIdAsync(member.UserId));
                }
			}
            var model = new Tuple<List<ClubMember>, List<ClubhouseUser>>(clubmembers, users);
            return View(model);
        }

		[Authorize(Roles = "Admin, ClubModerator")]
        // GET: ClubMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClubMember == null)
            {
                return NotFound();
            }

            var clubMember = await _context.ClubMember.FindAsync(id);
            if (clubMember == null)
            {
                return NotFound();
            }
            return View(clubMember);
        }

        [Authorize(Roles = "Admin, ClubModerator")]
        // POST: ClubMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ClubId")] ClubMember clubMember)
        {
            if (id != clubMember.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clubMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClubMemberExists(clubMember.Id))
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
            return View(clubMember);
        }

        [Authorize(Roles = "Admin, ClubModerator")]
        // GET: ClubMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context == null)
            {
                return NotFound();
            }

            var clubMember = await _context.ClubMember
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clubMember == null)
            {
                return NotFound();
            }

            return View(clubMember);
        }

        [Authorize(Roles = "Admin, ClubModerator")]
        // POST: ClubMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClubMember == null)
            {
                return Problem("Entity set 'ClubhouseContext.ClubMember'  is null.");
            }
            var clubMember = await _context.ClubMember.FindAsync(id);
            if (clubMember != null)
            {
                _context.ClubMember.Remove(clubMember);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClubMemberExists(int id)
        {
          return _context.ClubMember.Any(e => e.Id == id);
        }
    }
}
