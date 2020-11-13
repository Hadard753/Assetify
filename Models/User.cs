using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assetify.Models
{
    public class User
    {
        public int UserID { get; set; }
        //public int AddressID { get; set; }
        [Required,Remote(action: "VerifyEmail", controller: "Users")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        /*[Required(ErrorMessage = "Please enter confirm password")]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirm password does not match")]
        public string ComparePassword { get; set; }*/

        [Required, StringLength(20, ErrorMessage = "First Name can be up to 20 chars")]
        public string FirstName { get; set; }
        [Required, StringLength(20, ErrorMessage = "Last Name can be up to 20 chars")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Phone number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^05\d([-]{0,1})\d{7}$", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }
        public bool IsVerified { get; set; }

        public string ProfileImgPath { get; set; }

        public DateTime LastSeenFavorite { get; set; }
        public DateTime LastSeenMessages { get; set; }

        public ICollection<UserAsset> Assets { get; set; }
        public ICollection<Search> SearchHistory { get; set; }
        public bool IsAdmin { get; set; }
        //public Address Address { get; set; }

        [NotMapped]
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int NumOfFavorites { set; get; }

        [NotMapped]
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int NumOfPublish { set; get; }
    }
}
