using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GroupCapstoneProoj.Data;
using GroupCapstoneProoj.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace GroupCapstoneProoj.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AdminsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admins
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var admin = _context.Admins.Where(c => c.IdentityUserId == userId).FirstOrDefault();
            if (admin == null)
            {
                return NotFound();
            }
            else
            {
                return View(admin);

            }
        }

        // GET: Admins/Details/5
        public IActionResult Details(int? id)
        {
            var admin = _context.Admins
      .Include(c => c.IdentityUser)
      .FirstOrDefaultAsync(m => m.Id == id);
            if (admin == null)
            {
                return NotFound();
            }
            else
            {
                return View(admin);
            }
        }

        // GET: Admins/Create
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,IdentityUserId,FirstName,LastName,ZipCode")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                admin.IdentityUserId = userId;
                _context.Add(admin);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", admin.IdentityUserId);
            return View(admin);
        }

        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", admin.IdentityUserId);
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdentityUserId,FirstName,LastName,ZipCode")] Admin admin)
        {
            if (id != admin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.Id))
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
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", admin.IdentityUserId);
            return View(admin);
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .Include(a => a.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(int id)
        {
            return _context.Admins.Any(e => e.Id == id);
        }

        public async Task<IActionResult> DeleteTrader(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trader = await _context.Traders
                .Include(e => e.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trader == null)
            {
                return NotFound();
            }

            return View(trader);
        }
    }
}
