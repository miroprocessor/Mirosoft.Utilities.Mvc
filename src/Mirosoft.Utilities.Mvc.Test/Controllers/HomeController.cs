using Mirosoft.Utilities.Mvc.Test.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Mirosoft.Utilities.Mvc.Test.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(GetProducts());
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            ViewBag.Products = form["Products"];
            return View(GetProducts());
        }


        private List<Product> GetProducts()
        {
            return new List<Product>()
            {
                new Product(){Id = 1,Name = "Surface" },
                new Product(){Id = 2,Name = "iPad" },
                new Product(){Id = 3,Name = "iPhone" },
                new Product(){Id = 4,Name = "ThinkPad" },
                new Product(){Id = 5,Name = "Y50" },
                new Product(){Id = 6,Name = "AMD" }
            };
        }
    }
}