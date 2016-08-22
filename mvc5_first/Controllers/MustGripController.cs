using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc5_first.Controllers
{
    public class MustGripController : Controller
    {
        //
        // GET: /MustGrip/
        public ActionResult Index()
        {
            return View("MustGripView");
        }
	}
}