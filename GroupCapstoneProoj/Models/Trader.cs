using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GroupCapstoneProoj.Models
{
    public class Trader
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }

        [Required(ErrorMessage = "Please enter your first name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Goods & Services I Offer")]

        public string GoodsServicesAvailable { get; set; }

        [Required(ErrorMessage = "Please enter your street number and name")]
        [Display(Name = "Street Name")]
        public string StreetName { get; set; }

        [Required(ErrorMessage = "Please enter your city")]
        [Display(Name = "City")]

        public string City { get; set; }

        [Required]
        [Display(Name = "State")]

        public string State { get; set; }

        [Required(ErrorMessage = "Please enter your zip code"), MaxLength(5)]
        [Display(Name = "Zip Code")]

        public string ZipCode { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
                
        public string ProfilePicture { get; set; }
        public int Rating { get; set; }
    }
}
