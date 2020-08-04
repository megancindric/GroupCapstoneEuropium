using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCapstoneProoj.Models
{
    public class TraderIndexViewModel
    {
        public List<Listing> Listings { get; set; }
        public SelectList CategoryList = new SelectList(new List<string>() { "Goods", "Services", "All" });
        public string SelectedCategory { get; set; }
    }
}
