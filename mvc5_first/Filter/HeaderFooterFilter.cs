using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc5_first.ViewModels;

namespace mvc5_first.Filter
{
    public class HeaderFooterFilter:ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewResult vr = filterContext.Result as ViewResult;
            if (vr != null)
            {
                BaseViewModel bvm = vr.Model as BaseViewModel;
                if (bvm != null)
                {
                    bvm.UserName = HttpContext.Current.User.Identity.Name;
                    bvm.FooterData = new FooterViewModel();
                    bvm.FooterData.CompanyName = "MustGrip";
                    bvm.FooterData.Year = DateTime.Now.Year.ToString();
                }
                
            }

        }
    }
}