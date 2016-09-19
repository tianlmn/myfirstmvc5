using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc5_first.ViewModels
{
    public class BaseViewModel
    {
        public BaseViewModel()
        {
            FooterData = new FooterViewModel();
        }

        public string UserName { get; set; }
        public FooterViewModel FooterData { get; set; }//New Property
    }
}