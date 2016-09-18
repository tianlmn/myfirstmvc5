using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc5_first.ViewModels
{
    public class FileUploadViewModel : BaseViewModel
    {
        public HttpPostedFileBase fileUpload { get; set; }
    }
}