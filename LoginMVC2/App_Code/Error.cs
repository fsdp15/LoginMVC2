using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginMVC2.App_Code
{
    public class Error : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {

        }
    }
}