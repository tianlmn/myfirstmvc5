﻿using System;
using System.Web;
using System.Web.Mvc;

namespace mvc5_first
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            
            //filters.Add(new AuthorizeAttribute());
        }
    }
}
