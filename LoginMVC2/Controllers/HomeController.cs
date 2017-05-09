using LoginMVC2.Models;
using System.Web.Mvc;

namespace LoginMVC2.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

    }
}
