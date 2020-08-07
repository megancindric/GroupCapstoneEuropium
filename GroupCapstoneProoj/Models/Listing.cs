using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCapstoneProoj.Models
{
    public class Listing
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }

        [Required(ErrorMessage = "A listing name is required")]
        [Display(Name = "Listing Name")]

        public string ListingName { get; set; }

        [Required(ErrorMessage = "A category is required")]
        [Display(Name = "Category")]

        public string Category { get; set; }

        [Required(ErrorMessage = "Please enter a description of your listing")]
        [Display(Name = "Description")]

        public string ListingDescription { get; set; }

        [Required(ErrorMessage = "Please enter something you'd like in return")]
        [Display(Name = "Wanted in Return")]

        public string InReturn { get; set; }

        [Required(ErrorMessage = "A price value is required")]
        [Display(Name = "Price")]

        public double Price { get; set; }

        [Required(ErrorMessage = "Please enter your zip code"), MaxLength(5)]
        [Display(Name = "Zip Code")]

        public string ZipCode { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [Display(Name = "Listing Status")]

        public string ListingStatus { get; set; }

        public string imageOne { get; set; }
        public string imageTwo { get; set; }
        public string imageThree { get; set; }

        public string PurchasedBy { get; set; }

        public int BuyerRating { get; set; }
        public int SellerRating { get; set; }

    }
}
