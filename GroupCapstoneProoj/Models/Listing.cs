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
        public string ListingName { get; set; }

        public List<string> ListingPictures { get; set; }

        [Required(ErrorMessage = "A category is required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Please enter something you'd like in return")]
        public string InReturn { get; set; }

        [Required(ErrorMessage = "A price value is required")]
        public double Price { get; set; }

    }
}
