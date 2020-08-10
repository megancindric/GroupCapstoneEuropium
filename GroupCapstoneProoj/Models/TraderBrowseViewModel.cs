using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCapstoneProoj.Models
{
    public class TraderBrowseViewModel
    {
        public List<Listing> Listings { get; set; }

        public SelectList CategoryList = new SelectList(new List<string>() { "Goods", "Services", "All" });

        [Display(Name = "Selected Category")]

        public string SelectedCategory { get; set; }
    }
}
