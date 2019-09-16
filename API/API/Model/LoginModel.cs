using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class LoginModel
    {
        [Required(ErrorMessage = "username is required" )]
        public string Username { get; set; }

        [Required(ErrorMessage = "password is required")]
        public string Password { get; set; }
    }
}
