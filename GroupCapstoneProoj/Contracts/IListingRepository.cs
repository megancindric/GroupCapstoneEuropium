using GroupCapstoneProoj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCapstoneProoj.Contracts
{
    public interface IListingRepository : IRepositoryBase<Listing>
    {
        Listing GetListing(int listingId);
        void CreateListing(Listing listing);
    }
}
