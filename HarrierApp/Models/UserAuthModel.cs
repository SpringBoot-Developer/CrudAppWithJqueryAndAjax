using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarrierApp.Models
{
    public class UserAuthModel
    {

        public int Id { get; set; } 
        public string UserName { get; set; } 
        public string Email { get; set; } 
        public string Password { get; set; } 
        public bool IsValid { get; set; } 
    }
}