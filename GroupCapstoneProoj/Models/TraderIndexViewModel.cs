﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCapstoneProoj.Models
{
    public class TraderIndexViewModel
    {
        public List<Listing> MyListings { get; set; }
        public List<Listing> MyPurchases { get; set; }


        public Trader Trader { get; set; }
        public Listing Listing { get; set; }

    }
}
