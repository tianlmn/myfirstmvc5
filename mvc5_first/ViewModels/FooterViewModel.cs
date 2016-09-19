using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc5_first.ViewModels
{
    public class FooterViewModel
    {
        public FooterViewModel()
        {
            Year = "2016";
            CompanyName = "MustGrip";
        }
        public string CompanyName { get; set; }
        public string Year { get; set; }
    }
}