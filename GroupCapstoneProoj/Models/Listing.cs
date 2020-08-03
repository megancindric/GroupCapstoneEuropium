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

        [Required]
        public string ListingName { get; set; }

        public List<string> ListingPictures { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string InReturn { get; set; }

        [Required]
        public double Price { get; set; }

    }
}
