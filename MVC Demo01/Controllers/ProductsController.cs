using Microsoft.AspNetCore.Mvc;

namespace MVC_Demo01.Controllers
{
    public class ProductsController : Controller
    {
        // Model Binding 
        // From Form
        // From Route
        // From Route 
        // Query Strin 
        // From Header

        // Action => public nonStatic method
        public IActionResult   Get(int id , string name, Product product)
        {
            //ContentResult contentResult = new ContentResult();
            //contentResult.Content = $"Product {id}";
           // contentResult.ContentType = "object/pdf";
            return Content($"Product {id} : {name}", "text/html"); 
           
        }

        public IActionResult Redirect()
        {
           // RedirectResult redirectResult = new RedirectResult ("https://www.google.com");
            return Redirect("https://www.google.com");
        }

        public IActionResult RedirectToAction()
        {
           // RedirectToActionResult redirectResult = new RedirectToActionResult("Get", "Products", new {id = 10});
            return RedirectToAction(nameof(Get), new {id = 12});
        }
    }
}
