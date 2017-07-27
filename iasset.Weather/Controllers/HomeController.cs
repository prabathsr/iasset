using System.Web.Mvc;

namespace iasset.Weather.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.AppTitle = "iasset Weather View";
            return View();
        }
    }
}