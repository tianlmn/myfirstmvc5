using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvc5_first.Models
{
    public class UserDetails
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}