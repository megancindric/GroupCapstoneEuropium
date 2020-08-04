using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GroupCapstoneProoj.Data;
using GroupCapstoneProoj.Models;
using System.Security.Claims;

namespace GroupCapstoneProoj.Controllers
{
    public class TradersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TradersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Traders
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var trader = _context.Traders.Where(c => c.IdentityUserId == userId).FirstOrDefault();
            if (trader == null)
            {
                return NotFound();
            }
            else
            {
                return View(trader);

            }
        }

        // GET: Employees
        public ActionResult Browse()
        {
            TraderIndexViewModel viewModel = new TraderIndexViewModel();
            viewModel.SelectedCategory = "All";

            viewModel.Listings = new List<Listing>();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentTrader = _context.Traders.Where(s => s.IdentityUserId == userId).FirstOrDefault();
            var localListing = _context.Listings.Where(s => s.ZipCode == currentTrader.ZipCode).ToList();
            foreach(Listing listing in localListing)
            {
                viewModel.Listings.Add(listing);
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Browse(TraderIndexViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentTrader = _context.Traders.Where(s => s.IdentityUserId == userId).FirstOrDefault();
            viewModel.Listings = new List<Listing>();
            var currentListings = _context.Listings.Where(s => s.ZipCode == currentTrader.ZipCode).ToList();
            foreach (Listing listing in currentListings)
            {
                viewModel.Listings.Add(listing);
            }
            return View(viewModel);
        }

        // GET: Traders/Details/5
        public IActionResult Details(int? id)
        {

            var trader = _context.Traders
                .Include(c => c.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trader == null)
            {
                return NotFound();
            }
            else
            {
                return View(trader);
            }

        }

        // GET: Traders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Traders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,IdentityUserId,FirstName,LastName,PhoneNumber,StreetName,City,State,ZipCode,Latitude,Longitude")] Trader trader)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            trader.IdentityUserId = userId;
            //Google Maps API? customer = GeocodeCustomer(customer);
            _context.Add(trader);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Traders/Edit/5
        public IActionResult Edit()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var trader = _context.Traders.Where(c => c.IdentityUserId == userId).SingleOrDefault();

            return View(trader);
        }

        // POST: Traders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,IdentityUserId,FirstName,LastName,PhoneNumber,StreetName,City,State,ZipCode,Latitude,Longitude")] Trader trader)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var traderToUpdate = _context.Traders.Where(c => c.IdentityUserId == userId).SingleOrDefault();
            try
            {
                traderToUpdate.FirstName = trader.FirstName;
                traderToUpdate.LastName = trader.LastName;
                traderToUpdate.PhoneNumber = trader.PhoneNumber;
                traderToUpdate.StreetName = trader.StreetName;
                traderToUpdate.City = trader.City;
                traderToUpdate.State = trader.State;
                traderToUpdate.ZipCode = trader.ZipCode;
                //traderToUpdate = GeocodeCustomer(traderToUpdate);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TraderExists(trader.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Traders/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trader = _context.Traders
                .Include(c => c.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trader == null)
            {
                return NotFound();
            }

            return View(trader);
        }

        // POST: Traders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var trader = _context.Traders.FirstOrDefault(m => m.Id == id);
            _context.Traders.Remove(trader);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool TraderExists(int id)
        {
            return _context.Traders.Any(e => e.Id == id);
        }

        private bool ListingExists(int id)
        {
            return _context.Listings.Any(e => e.Id == id);
        }

        public IActionResult CreateListing()
        {
            return View();
        }

        // POST: Listing/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateListing([Bind("Id,IdentityUserId,ListingName,Category,InReturn,Price,StreetName,City,State,ZipCode,Latitude,Longitude")] Listing listing)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            listing.IdentityUserId = userId;
            _context.Add(listing);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditListing(int id)
        {
            //When we create button for this, pass in ID of current listing as param
            var listing = _context.Listings.Where(c => c.Id == id).SingleOrDefault();
            return View(listing);
        }

        // POST: Listing/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditListing(int id, [Bind("Id,IdentityUserId,ListingName,Category,InReturn,Price,StreetName,City,State,ZipCode,Latitude,Longitude")] Listing listing)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var listingToUpdate = _context.Listings.Where(c => c.Id == id).SingleOrDefault();
            try
            {
                listingToUpdate.ListingName = listing.ListingName;
                listingToUpdate.Category = listing.Category;
                listingToUpdate.Price = listing.Price;
                listingToUpdate.InReturn = listing.InReturn;
                listingToUpdate.StreetName = listing.StreetName;
                listingToUpdate.City = listing.City;
                listingToUpdate.State = listing.State;
                listingToUpdate.ZipCode = listing.ZipCode;
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListingExists(listing.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Traders/Delete/5
        public IActionResult DeleteListing(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listing = _context.Listings
                .Include(c => c.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listing == null)
            {
                return NotFound();
            }

            return View(listing);
        }

        // POST: Traders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteListingConfirmed(int id)
        {
            var listing = _context.Listings.FirstOrDefault(m => m.Id == id);
            _context.Listings.Remove(listing);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ListingDetails(int? id)
        {

            var listing = _context.Traders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listing == null)
            {
                return NotFound();
            }
            else
            {
                return View(listing);
            }

        }

    }
}
