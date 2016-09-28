using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.SPA
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            FooterData=new FooterViewModel();
        }

        public string UserName { get; set; }
        public FooterViewModel FooterData { get; set; }//New Property
    }
}
