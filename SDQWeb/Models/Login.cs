using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDQWeb.Models
{
    public class Login
    {
        [Display(Name ="Email ")]
        [Required(ErrorMessage = "Email Required")]
        public string Email{ get; set; }

        [Required(ErrorMessage ="Password Requires")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Remember")]
        public bool Remember { get; set; }


    }
}