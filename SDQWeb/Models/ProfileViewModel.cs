using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDQWeb.Models
{
    public class ProfileViewModel
    {
        public string Image { get; set; }

        public string Name { get; set; }

        public int UserId { get; set; }

        public string DateOfBirth   { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Gender { get; set; }
        public string PresentAddress { get; set; }
        public string Address { get; set; }
        List<Address> addresses = new List<Address>();
    }
}