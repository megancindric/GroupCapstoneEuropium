using GroupCapstoneProoj.Contracts;
using GroupCapstoneProoj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCapstoneProoj.Data
{
    public class ListingRepository : RepositoryBase<Listing>, IListingRepository
    {
        public ListingRepository(ApplicationDbContext applicationDbContext) :base(applicationDbContext)
        {

        }
        public Listing GetListing(int listingId) => FindByCondition(c => c.Id.Equals(listingId)).SingleOrDefault();

        public void CreateListing(Listing listing) => Create(listing);
    }
}
