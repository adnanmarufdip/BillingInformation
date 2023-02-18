using BillingInformation.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BillingInformation.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext dataContext;

        public HomeController()
        {
            this.dataContext = new DataContext();
        }

        // GET: Products
        [HttpGet]
        public ActionResult Index()
        {
            var products = dataContext.GetProducts();
            ViewBag.productData = JsonConvert.SerializeObject(products);
            return View(products);
        }
    }
}
