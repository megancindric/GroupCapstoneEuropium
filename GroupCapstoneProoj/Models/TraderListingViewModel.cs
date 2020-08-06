using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCapstoneProoj.Models
{
    public class TraderListingViewModel
    {
        public Listing CurrentListing { get; set; }
        public Trader CurrentSeller { get; set; }
        public Trader CurrentBuyer { get; set; }
    }
}
