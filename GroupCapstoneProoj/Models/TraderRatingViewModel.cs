using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GroupCapstoneProoj.Models
{
    public class TraderRatingViewModel
    {
        public List<Trader> Trader { get; set; }
        public Listing Listing { get; set; }
    }
}
