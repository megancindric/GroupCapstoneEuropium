using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCapstoneProoj.Models
{
    public class AdminIndexViewModel
    {
        public List<Trader> Traders { get; set; }
        public List<Listing> Listings { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
