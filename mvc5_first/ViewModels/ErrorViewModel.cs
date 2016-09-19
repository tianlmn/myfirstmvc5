using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc5_first.ViewModels
{
    public class ErrorViewModel:BaseViewModel
    {
        public string Error { get; set; }

        public string Stack { get; set; }
    }
}