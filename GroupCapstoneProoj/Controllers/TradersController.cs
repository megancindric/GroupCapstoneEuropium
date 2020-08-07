namespace GroupCapstoneProoj.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using CloudinaryDotNet.Actions;
    using GroupCapstoneProoj.Data;
    using GroupCapstoneProoj.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.VisualBasic;
    using Stripe;
    using Stripe.Radar;
    using GoogleMaps.LocationServices;


    public class TradersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TradersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Traders
        public ActionResult Index()
        {
            TraderIndexViewModel viewModel = new TraderIndexViewModel();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            viewModel.MyListings = new List<Listing>();
            viewModel.Trader = _context.Traders.Where(s => s.IdentityUserId == userId).FirstOrDefault();
            viewModel.MyListings = _context.Listings.Where(s => s.IdentityUserId == userId && s.ListingStatus != "Archived").ToList();
            viewModel.MyPurchases = _context.Listings.Where(s => s.PurchasedBy == userId).ToList();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(TraderIndexViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            viewModel.Trader = _context.Traders.Where(s => s.IdentityUserId == userId).FirstOrDefault();
            var myListings = _context.Listings.Where(s => s.IdentityUserId == userId && s.ListingStatus != "Archived").ToList();
            var myPurchases = _context.Listings.Where(s => s.PurchasedBy == userId).ToList();

            foreach (Listing listing in myListings)
            {
                viewModel.MyListings.Add(listing);
            }
            foreach (Listing listing in myPurchases)
            {
                viewModel.MyPurchases.Add(listing);
            }
            return View(viewModel);
        }

        // GET: Employees
        public ActionResult Browse()
        {
            TraderBrowseViewModel viewModel = new TraderBrowseViewModel();
            viewModel.SelectedCategory = "All";

            viewModel.Listings = new List<Listing>();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentTrader = _context.Traders.Where(s => s.IdentityUserId == userId).FirstOrDefault();
            var localListing = _context.Listings.Where(s => s.ZipCode == currentTrader.ZipCode && s.ListingStatus != "Archived").ToList();
            foreach (Listing listing in localListing)
            {
                viewModel.Listings.Add(listing);
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Browse(TraderBrowseViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentTrader = _context.Traders.Where(s => s.IdentityUserId == userId).FirstOrDefault();
            viewModel.Listings = new List<Listing>();
            if(viewModel.SelectedCategory == "All")
            {
               var currentListings = _context.Listings.Where(s => s.ZipCode == currentTrader.ZipCode && s.ListingStatus != "Archived").ToList();
                foreach (Listing listing in currentListings)
                {
                    viewModel.Listings.Add(listing);
                }
            }
            else
            {
                var currentListings = _context.Listings.Where(s => s.ZipCode == currentTrader.ZipCode && s.Category == viewModel.SelectedCategory && s.ListingStatus != "Archived").ToList();
                foreach (Listing listing in currentListings)
                {
                    viewModel.Listings.Add(listing);
                }
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
        public IActionResult Create([Bind("Id,IdentityUserId,FirstName,LastName,GoodsServicesAvailable,PhoneNumber,StreetName,City,State,ZipCode,Latitude,Longitude")] Trader trader, IFormFile files)
        {
            var fileName = UploadImage(files);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            trader.IdentityUserId = userId;
            trader.ProfilePicture = fileName;
            _context.Add(trader);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Traders/Edit/5
        public IActionResult Edit()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var trader = _context.Traders.Where(c => c.IdentityUserId == userId).FirstOrDefault();

            return View(trader);
        }

        // POST: Traders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,IdentityUserId,FirstName,LastName,GoodsServicesAvailable,PhoneNumber,StreetName,City,State,ZipCode,Latitude,Longitude")] Trader trader, IFormFile files)
        {

            var fileName = UploadImage(files);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var traderToUpdate = _context.Traders.Where(c => c.IdentityUserId == userId).SingleOrDefault();
            try
            {
                traderToUpdate.FirstName = trader.FirstName;
                traderToUpdate.LastName = trader.LastName;
                traderToUpdate.PhoneNumber = trader.PhoneNumber;
                traderToUpdate.GoodsServicesAvailable = trader.GoodsServicesAvailable;
                traderToUpdate.StreetName = trader.StreetName;
                traderToUpdate.City = trader.City;
                traderToUpdate.State = trader.State;
                traderToUpdate.ZipCode = trader.ZipCode;
                traderToUpdate.ProfilePicture = fileName;
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

        public string UploadImage(IFormFile files)
        {
            var myFileName = "";
            if (files != null)
            {

                if (files.Length > 0)
                {
                    var fileName = Path.GetFileName(files.FileName);
                    myFileName = Convert.ToString(Guid.NewGuid());
                    var fileExtension = Path.GetExtension(fileName);
                    var newFileName = string.Concat(myFileName, fileExtension);
                    var filePath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img")).Root + $@"\{newFileName}";

                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        files.CopyTo(fs);
                        fs.Flush();
                    }
                    return newFileName;
                }

            }
            return myFileName;
        }

        public List<string> UploadImage(List<IFormFile> files)
        {
            var myFileName = new List<string>();
            if (files != null)
            {
                foreach (var item in files)
                {
                    if (item.Length > 0)
                    {
                        var fileName = Path.GetFileName(item.FileName);
                        var fileNameString = Convert.ToString(Guid.NewGuid());
                        var fileExtension = Path.GetExtension(fileName);
                        var newFileName = string.Concat(fileNameString, fileExtension);
                        var filePath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ListingImgs")).Root + $@"\{newFileName}";

                        using (FileStream fs = System.IO.File.Create(filePath))
                        {
                            item.CopyTo(fs);
                            fs.Flush();
                        }
                        myFileName.Add(newFileName);

                    }
                }
            }
            return myFileName;
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
        public IActionResult CreateListing([Bind("Id,IdentityUserId,ListingName,Category,InReturn,Price,StreetName,ListingDescription,PurchasedBy,BuyerRating,SellerRating,City,State,ZipCode,Latitude,Longitude")] Listing listing, List<IFormFile> files)
        {
            List<string> imageList = UploadImage(files);
            listing.imageOne = imageList[0];
            listing.imageTwo = imageList[1];
            listing.imageThree = imageList[2];
            listing.ListingStatus = "Active";

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            listing.IdentityUserId = userId;
            listing = GeocodeListing(listing);
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
        public IActionResult EditListing(int id, [Bind("Id,IdentityUserId,ListingName,Category,InReturn,ListingDescription,PurchasedBy,BuyerRating,SellerRating,Price,ZipCode,Latitude,Longitude")] Listing listing)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var listingToUpdate = _context.Listings.Where(c => c.Id == id).SingleOrDefault();
            try
            {
                listingToUpdate.ListingName = listing.ListingName;
                listingToUpdate.Category = listing.Category;
                listingToUpdate.Price = listing.Price;
                listingToUpdate.ListingDescription = listing.ListingDescription;
                listingToUpdate.InReturn = listing.InReturn;
                listingToUpdate.ZipCode = listing.ZipCode;
                listing = GeocodeListing(listing);
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var buyer = _context.Traders.Where(s => s.IdentityUserId == userId).FirstOrDefault();
            var listing = _context.Listings.Where(m => m.Id == id).FirstOrDefault();
            var seller = _context.Traders.Where(s => s.IdentityUserId == listing.IdentityUserId).FirstOrDefault();
            if (listing == null)
            {
                return NotFound();
            }
            else
            {
                TraderListingViewModel viewModel = new TraderListingViewModel();
                viewModel.CurrentListing = listing;
                viewModel.CurrentSeller = seller;
                viewModel.CurrentBuyer = buyer;

                return View(viewModel);
            }
        }

        public ActionResult MakePayment(int?id)
        {


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var trader = _context.Traders.Where(s => s.IdentityUserId == userId).FirstOrDefault();

            var listing = _context.Listings.Where(l => l.Id == id).FirstOrDefault();

            //var options = new RequestOptions
            //{
            //    ApiKey = "sk_test_51H7pRTHVSJCHPuZaNpLqbbw6nUXpWWOYvvS6o3dNggkdoq0JX0WPqeFjXiHmlkfnOBkgcjC1kHBJklmEuyXO8Yyj00y0tEtWPL"
            //};
            //var service = new ChargeService();
            //Charge charge = service.Get("ch_1H7xFAHVSJCHPuZaaMbsiJLS", options);

            Stripe.StripeConfiguration.ApiKey = "sk_test_51H7pRTHVSJCHPuZaNpLqbbw6nUXpWWOYvvS6o3dNggkdoq0JX0WPqeFjXiHmlkfnOBkgcjC1kHBJklmEuyXO8Yyj00y0tEtWPL";
            Stripe.CreditCardOptions card = new Stripe.CreditCardOptions();
            card.Name = trader.FirstName + " " + trader.LastName;
            card.Number = "4242424242424242";
            card.ExpMonth = 9;
            card.ExpYear = 2021;
            card.Cvc = "122";

            Stripe.TokenCreateOptions tokenCreateOption = new Stripe.TokenCreateOptions();
            tokenCreateOption.Card = card;
            Stripe.TokenService tokenService = new Stripe.TokenService();
            Stripe.Token token = tokenService.Create(tokenCreateOption);

            Stripe.CustomerCreateOptions customer = new Stripe.CustomerCreateOptions();
            customer.Source = token.Id;
            var custService = new Stripe.CustomerService();
            Stripe.Customer stpCustomer = custService.Create(customer);

            var options = new Stripe.ChargeCreateOptions
            {
                Amount = (Convert.ToInt32(listing.Price) * 100),
                Currency = "USD",
                Customer = stpCustomer.Id,
                Description = listing.ListingName,
            };

            var service = new Stripe.ChargeService();
            Stripe.Charge charge = service.Create(options);

            var status = charge.Status;

            if (status == "succeeded")
            {
                listing.ListingStatus = "InProgress";
                listing.PurchasedBy = trader.IdentityUserId;
                _context.Update(listing);
                _context.SaveChanges();
                return RedirectToAction("CompleteTransaction", new { id = id });
            }
            else
            {

                return RedirectToAction("FailedTransaction");
            }

        }

        public Listing GeocodeListing(Listing listing)
        {
            AddressData address = new AddressData
            {

                Zip = listing.ZipCode
            };
            var geocodeRequest = new GoogleLocationService("AIzaSyAItJAdyunI8yeyDG6JrIhREocXwsgYN9k");
            var latlong = geocodeRequest.GetLatLongFromAddress(address);

            listing.Latitude = latlong.Latitude;
            listing.Longitude = latlong.Longitude;
            return listing;

        }

        public ActionResult CompleteTransaction(int id)
        {
            var listing = _context.Listings.Where(c => c.Id == id).SingleOrDefault();
            return View(listing);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CompleteTransaction(int id, string starValue)
        {
            var listing = _context.Listings.Where(c => c.Id == id).SingleOrDefault();
            var seller = _context.Traders.Where(t => t.IdentityUserId == listing.IdentityUserId).FirstOrDefault();
            listing.SellerRating = Int32.Parse(starValue);
            listing.ListingStatus = "InProgress";
            seller.Rating = Int32.Parse(starValue);
            _context.Listings.Update(listing);
            _context.Traders.Update(seller);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult RateBuyer(int id)
        {
            var listing = _context.Listings.Where(c => c.Id == id).SingleOrDefault();
            return View(listing);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RateBuyer(int id, string starValue)
        {
            var listing = _context.Listings.Where(c => c.Id == id).SingleOrDefault();
            var buyer = _context.Traders.Where(t => t.IdentityUserId == listing.PurchasedBy).FirstOrDefault();
            listing.BuyerRating = Int32.Parse(starValue);
            listing.ListingStatus = "Archived";
            buyer.Rating = Int32.Parse(starValue);
            _context.Listings.Update(listing);
            _context.Traders.Update(buyer);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
