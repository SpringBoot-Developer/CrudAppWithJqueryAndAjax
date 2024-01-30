using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HarrierApp.Models
{
    public class UsersModel
    {
        public int Id { get; set; }

       
        public string Name { get; set; }

      
        public string Email { get; set; }


        public string Mobile { get; set; }


        public List<string>  Skills { get; set; }

        public string Role { get; set; }


        public string Status { get; set; }

        public string Password { get; set; }


        public string ConfirmPassword { get; set; }
    }

}