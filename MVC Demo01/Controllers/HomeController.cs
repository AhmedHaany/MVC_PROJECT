using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace MVC_Demo01.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult  Index() => View();
       // {
        //    ContentResult contentResult = new ContentResult();
        //    contentResult.Content = "Hello From Index";

        //    contentResult.ContentType = "text/html";

        //    return contentResult ;

            //return View();
        //
    
        public IActionResult Privacy() => View();
        public IActionResult AboutUs() => View();
        public IActionResult ContactUs() => View();
    
    }
}
