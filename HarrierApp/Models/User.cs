using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HarrierApp.Models
{
    public class User
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        //Required(ErrorMessage = "Email is required")]
        //[EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Mobile is required")]
        //[RegularExpression(@"^\d{10}$" , ErrorMessage = "Invalid Mobile Number")]
        public string Mobile { get; set; }

       // [Required(ErrorMessage = "At least 2 skills should be selected")]
        public List<string>  Skills { get; set; }
//
       // [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }

       // [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }
//
       //[Required(ErrorMessage = "Password is required")]
//[MinLength(4 , ErrorMessage = "Password should have at least 4 characters")]
        //RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{4,}$" , ErrorMessage = "Password should contain at least one number, one lowercase, and one uppercase letter")]
        public string Password { get; set; }

        //[Compare("Password" , ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }

}